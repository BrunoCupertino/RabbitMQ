using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Service
{
    public interface IBus
    {
        void Publish<T>(T message);
        void Subscribe<T>(Action<T> callBackFunction);
    }
}
