using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaymentGateway.WebAPI.Controllers
{
    public class CardsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return Holder.Cards;
        }

        [HttpGet]
        public Card Get(string card_number)
        {
            return Holder.Cards.FirstOrDefault(c => c.Card_number == card_number);
        }

        [HttpPost]
        public void Post(Card card)
        {
            Holder.CreateCard(card);
        }

        [HttpDelete]
        public void Delete(string card_number)
        {
            Holder.DeleteCard(Holder.Cards.FirstOrDefault(c => c.Card_number == card_number));
        }

        //[HttpPut]
        //public void Edit(Card card)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
