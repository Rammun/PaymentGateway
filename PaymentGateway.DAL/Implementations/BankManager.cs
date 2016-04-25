using PaymentGateway.DAL.Interfaces;
using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.DAL.Implementations
{
    public class BankManager : IBankManager
    {
        ICardRepository cards;
        ITransactionRepository transactions;

        public BankManager()
        {
            
        }

        public ICardRepository Cards
        {
            get
            {
                if (cards == null)
                    cards = new FakeCardRepository();
                return cards;
            }
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (transactions == null)
                    transactions = new FakeTransactionRepository();
                return transactions;
            }
        }
    }
}
