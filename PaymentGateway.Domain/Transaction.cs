using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Card Card { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
