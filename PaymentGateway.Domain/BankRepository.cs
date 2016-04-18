using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public static class BankRepository
    {
        private static List<Card> cards;
        private static ReadOnlyCollection<Card> cardsReadOnly;

        private static List<Transaction> transactions;
        private static ReadOnlyCollection<Transaction> transactionReadOnly;

        static BankRepository()
        {
            cards = new List<Card>
            {
                new Card ("1234567890123456", 1, 2020, "001", "R"),
                new Card ("6543210987654321", 2, 2021, "011"),
                new Card ("0987654321234567", 3, 2022, "111", "", 30000000),
                new Card ("1234567890987654", 4, 2023, "002", "Ktoto"),
                new Card ("1029384756473829", 5, 2024, "022", "This is I", 1000000),
            };
            cardsReadOnly = new ReadOnlyCollection<Card>(cards);

            transactions = new List<Transaction>();
            transactionReadOnly = new ReadOnlyCollection<Transaction>(transactions);
        }


        public static ReadOnlyCollection<Card> Cards { get { return cardsReadOnly; } }
        public static ReadOnlyCollection<Transaction> Transactions { get { return transactionReadOnly; } }

        public static void AddCard(Card card)
        {
            if (cards.FirstOrDefault(c => c.Card_number == card.Card_number) != null)
                throw new Exception(string.Format("Карта с номером {0} существует", card.Card_number));
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
