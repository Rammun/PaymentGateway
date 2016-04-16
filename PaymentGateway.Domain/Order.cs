using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Payer { get; set; }
        public bool Paid { get; set; }
    }
}
