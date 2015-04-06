using Order.Domain.Entity;
using Order.Domain.Service;
using Order.Infrasctructure.Messaging;
using System;

namespace OrderProcessorReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageClient messageClient = new MessageRabbitMQClient();

            IBus bus = new Bus(messageClient);

            bus.Subscribe<NewProductOrderCommand>(message => 
            {
                Console.WriteLine(string.Format("Preparing {0}", message.ProductName));

                System.Threading.Thread.Sleep(2000);

                Console.WriteLine(string.Format("{0}'s {1} is ready.", message.ClientName, message.ProductName));
                Console.WriteLine("--------");
            });            
        }
    }
}
