using Order.Domain.Entity;
using Order.Infrasctructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Service
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IBus bus;

        public OrderProcessor(IBus bus)
        {
            this.bus = bus;
        }

        public void Process(NewProductOrderCommand order)
        {
            this.bus.Publish<NewProductOrderCommand>(order);
        }
    }
}
