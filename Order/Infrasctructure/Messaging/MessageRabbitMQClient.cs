using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrasctructure.Messaging
{
    public class MessageRabbitMQClient : IMessageClient
    {
        public string QueueName
        {
            get { return "myQueue"; }
        }

        private byte[] ObjectToByteArray<T>(T message)
        {
            if (message == null)
            {
                return null;
            }

            var bf = new BinaryFormatter();

            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, message);

                return ms.ToArray();
            }
        }

        private T ByteArrayToObject<T>(byte[] byteArrayMessage)
        {
            var binForm = new BinaryFormatter();

            using (var ms = new MemoryStream())
            {
                ms.Write(byteArrayMessage, 0, byteArrayMessage.Length);

                ms.Seek(0, SeekOrigin.Begin);

                T obj = (T)binForm.Deserialize(ms);

                return obj;
            }
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = "DW10CZL86W1",
                VirtualHost = "order",
                UserName = "user",
                Password = "user"
            };
        }

        public void Send<T>(T message)
        {
            var factory = this.CreateConnectionFactory();

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(this.QueueName, false, false, false, null);

                    var body = this.ObjectToByteArray<T>(message);

                    channel.BasicPublish("", this.QueueName, null, body);
                }
            }
        }

        public void Receive<T>(Action<T> callBackFunction)
        {
            var factory = this.CreateConnectionFactory();

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(this.QueueName, false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(this.QueueName, true, consumer);

                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;

                        var message = this.ByteArrayToObject<T>(body);

                        callBackFunction.Invoke(message);
                    }
                }
            }
        }
    }
}
