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
                    orders = new FakeOrderRepository();
                return orders;
            }
        }

        public decimal Cash { get; set; }
    }
}
