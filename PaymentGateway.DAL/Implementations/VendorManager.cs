using PaymentGateway.DAL.Interfaces;
using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Implementations
{
    public class VendorManager : IVendorManager
    {
        IOrderRepository orders;

        public VendorManager()
        {
            
        }

        public IOrderRepository Orders
        {
            get
            {
                if(orders == null)
                    orders = FakeOrderRepository.Instance;
                return orders;
            }
        }

        public static decimal Cash { get; set; }
    }
}
