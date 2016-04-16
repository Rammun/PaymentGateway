using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public static class Holder
    {
        static List<Card> cards = new List<Card>();
        static ReadOnlyCollection<Card> cardsReadOnly = new ReadOnlyCollection<Card>(cards);

        static List<Transaction> transactions = new List<Transaction>();
        static ReadOnlyCollection<Transaction> transactionReadOnly = new ReadOnlyCollection<Transaction>(transactions);

        public static ReadOnlyCollection<Card> Cards { get { return cardsReadOnly; } }
        public static ReadOnlyCollection<Transaction> Transaction { get { return transactionReadOnly; } }

        public static void CreateCard(Card card)
        {
            cards.Add(card);
        }

        public static void DeleteCard(Card card)
        {
            cards.Remove(card);
        }

        public static void CreateTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public static void DeleteTransaction(Transaction transaction)
        {
            transactions.Remove(transaction);
        }
    }
}
