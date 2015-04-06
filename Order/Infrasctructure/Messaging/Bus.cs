using Order.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrasctructure.Messaging
{
    public class Bus : IBus
    {
        private readonly IMessageClient messageClient;

        public Bus(IMessageClient messageClient)
        {
            this.messageClient = messageClient;
        }

        public void Publish<T>(T message)
        {
            this.messageClient.Send(message);
        }

        public void Subscribe<T>(Action<T> callBackFunction)
        {
            this.messageClient.Receive(callBackFunction);
        }
    }
}
