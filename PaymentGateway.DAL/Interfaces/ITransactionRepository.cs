using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        Transaction GetByOrderId(int id);
    }
}
