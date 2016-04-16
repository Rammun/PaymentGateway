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
        public string Pay(Transaction transaction)
        {
            Bank.AddTransaction(transaction);

            var vendorOrder = Vendor.Orders.FirstOrDefault(o => o.Id == transaction.Order_id);            
            if (vendorOrder == null)
            {
                transaction.Status = TransactionStatus.Error;
                return "Заказа с таким номером не существует";
            }                

            var consumerCard = Bank.Cards.FirstOrDefault(c => (c.Card_number == transaction.Card_number &&
                                                                 c.Expiry_year == transaction.Expiry_year &&
                                                                 c.Expiry_month == transaction.Expiry_month &&
                                                                 c.Cvv == transaction.Cvv &&
                                                                 c.Cardholder_name == transaction.Cardholder_name));
            if (consumerCard == null)
            {
                transaction.Status = TransactionStatus.Error;
                return "Карты с такими данными не существует";
            }                

            if (DateTime.Now >= new DateTime(consumerCard.Expiry_year, consumerCard.Expiry_month, 1))
            {
                transaction.Status = TransactionStatus.Error;
                return "Истек срок действия карты";
            }

            if (consumerCard.Cash_limit != null && consumerCard.Cash_limit < transaction.Amount_kop)
            {
                transaction.Status = TransactionStatus.Error;
                return "Недостаточно средств на счету";
            }                

            if(consumerCard.Cash_limit != null)
            {
                consumerCard.Cash_limit -= transaction.Amount_kop;
            }
            Vendor.Cash += transaction.Amount_kop;
            transaction.Status = TransactionStatus.OK;
            return "Транзакция проведена успешно";
        }

        [HttpGet]
        public string GetStatus(int order_id)
        {
            var transaction = Bank.Transaction.FirstOrDefault(t => t.Order_id == order_id);
            if (transaction == null)
                return "Отсутствует транзакция на этот заказ";
            return transaction.Status.ToString();
        }

        [HttpPut]
        public string Refund(int order_id)
        {
            var transaction = Bank.Transaction.FirstOrDefault(t => t.Order_id == order_id);
            if(transaction == null)
                return "Отсутствует транзакция на этот заказ";

            var card = Bank.Cards.FirstOrDefault(c => c.Card_number == transaction.Card_number);

            Vendor.Cash -= transaction.Amount_kop;
            if (card.Cash_limit != null)
                card.Cash_limit += transaction.Amount_kop;
            transaction.Status = TransactionStatus.Сancel;
            return "Произведен возврат средств";
        }

    }
}
