using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaymentGateway.WebAPI.Controllers
{
    public class PaymentGatewayController : ApiController
    {
        [HttpPost]
        public string Pay(Order order)
        {
            var vendorOrder = Vendor.Orders.FirstOrDefault(o => o.Id == order.Id);            
            if(vendorOrder == null)
                return "Заказа с таким номером не существует";

            var consumerCard = Holder.Cards.FirstOrDefault(c => (c.Card_number == order.Card.Card_number &&
                                                                    c.Expiry_year == order.Card.Expiry_year &&
                                                                    c.Expiry_month == order.Card.Expiry_month &&
                                                                    c.Cvv == order.Card.Cvv &&
                                                                    c.Cardholder_name == order.Card.Cardholder_name));
            if(consumerCard == null)
                return "Карты с таким номером не существует";

            if (DateTime.Now >= new DateTime(consumerCard.Expiry_year, consumerCard.Expiry_month, 1))
                return "Истек срок действия карты";

            if (consumerCard.Cash_limit != null && consumerCard.Cash_limit < order.Amount)
                return "Недостаточно средств на счету";



        }

        [HttpGet]
        public void GetStatus(int order_id)
        {

        }

        [HttpDelete]
        public void Refund(int order_id)
        {

        }

    }
}
