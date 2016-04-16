using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public static class Vendor
    {
        public static ICollection<Order> Orders { get; private set; }
        public static decimal Cash { get; set; }

        static Vendor()
        {
            Orders = new List<Order>();
        }
    }
}
