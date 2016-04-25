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
    public sealed class FakeCardRepository : Singleton<FakeCardRepository>, ICardRepository
    {
        private List<Card> cards;
        private ReadOnlyCollection<Card> cardsReadOnly;

        private FakeCardRepository()
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
        }
        
        public IEnumerable<Card> GetAll()
        {
            return cardsReadOnly;
        }

        public Card GetById(string card_number)
        {
            if (card_number == null)
                throw new NullReferenceException("Параметр card_number равен null");
            return cards.Find(c => c.Card_number == card_number);
        }

        public void Add(Card card)
        {
            if (card == null)
                throw new NullReferenceException("Параметр card равен null");

            if (cards.Any(c => c.Card_number == card.Card_number))
                throw new Exception(string.Format("Карта с номером {0} существует", card.Card_number));

            cards.Add(card);
        }

        public void Update(Card card)
        {
            throw new NotImplementedException();
        }

        public void Delete(string card_number)
        {
            var card = cards.FirstOrDefault(c => c.Card_number == card_number);
            if (card == null)
                throw new Exception("Попытка удалить несуществующую карту");
            cards.Remove(card);
        }
    }
}
