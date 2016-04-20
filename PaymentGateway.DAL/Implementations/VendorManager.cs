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
        public ICollection<Order> Orders
        {
            get { throw new NotImplementedException(); }
        }

        public decimal Cash
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
