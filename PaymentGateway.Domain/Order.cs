using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Order
    {
        private static int count;

        public Order()
        {
            this.Id = ++count;
        }

        public int Id { get; private set; }
        public decimal Amount { get; set; }
        public decimal Received { get; set; }
        public int TransactionId { get; set; }
    }
}
