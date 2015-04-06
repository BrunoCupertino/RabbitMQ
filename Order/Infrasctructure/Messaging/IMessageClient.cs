using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrasctructure.Messaging
{
    public interface IMessageClient
    {
        string QueueName { get; }
        void Send<T>(T message);
        void Receive<T>(Action<T> callBackFunction);
    }
}
