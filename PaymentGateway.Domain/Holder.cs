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
        private static List<Card> cards = new List<Card>();
        private static ReadOnlyCollection<Card> cardsReadOnly = new ReadOnlyCollection<Card>(cards);

        private static List<Transaction> transactions = new List<Transaction>();
        private static ReadOnlyCollection<Transaction> transactionReadOnly = new ReadOnlyCollection<Transaction>(transactions);

        public static ReadOnlyCollection<Card> Cards { get { return cardsReadOnly; } }
        public static ReadOnlyCollection<Transaction> Transaction { get { return transactionReadOnly; } }

        public static void AddCard(Card card)
        {
            cards.Add(card);
        }

        public static void DeleteCard(string card_number)
        {
            var card = cards.FirstOrDefault(c => c.Card_number == card_number);
            if (card == null)
                throw new Exception("Попытка удалить несуществующую карту");
            cards.Remove(card);
        }

        public static void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public static void DeleteTransaction(int transaction_id)
        {
            var transaction = transactions.FirstOrDefault(t => t.Id == transaction_id);
            if (transaction == null)
                throw new Exception("Попытка удалить несуществующую транзакцию");
            transactions.Remove(transaction);
        }
    }
}
