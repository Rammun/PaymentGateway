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
        ReadOnlyCollection<Card> Cards { get; }
        ReadOnlyCollection<Transaction> Transactions { get; }
        void AddCard(Card card);
        void DeleteCard(string card_number);
        void AddTransaction(Transaction transaction);
        void DeleteTransaction(int transaction_id);
    }
}
