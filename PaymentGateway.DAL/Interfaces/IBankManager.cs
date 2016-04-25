using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Interfaces
{
    public interface IBankManager
    {
        ICardRepository Cards { get; }
        ITransactionRepository Transactions { get; }
    }
}
