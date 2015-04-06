using Order.Domain.Entity;
using Order.Domain.Service;
using Order.Infrasctructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessorSender
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageClient messageClient = new MessageRabbitMQClient();

            IBus bus = new Bus(messageClient);

            IOrderProcessor orderProcessor = new OrderProcessor(bus);

            var newOrderCommand = new NewProductOrderCommand("Café com Leite", "João Travolta", 100.50M);            

            orderProcessor.Process(newOrderCommand);
        }
    }
}
