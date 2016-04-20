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

        public BankManager(ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            this.cards = cardRepository;
            this.transactions = transactionRepository;
        }
        public static ReadOnlyCollection<Card> Cards { get { return cards.GetALL(); } }
        public static ReadOnlyCollection<Transaction> Transactions { get { return transactions.GetAll(); } }

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
