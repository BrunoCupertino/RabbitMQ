using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entity
{
    [Serializable]
    public class NewProductOrderCommand
    {
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public decimal Value { get; set; }

        public NewProductOrderCommand(string productName, string clientName, decimal value)
        {
            this.ProductName = productName;
            this.ClientName = clientName;
            this.Value = value;
        }
    }
}
