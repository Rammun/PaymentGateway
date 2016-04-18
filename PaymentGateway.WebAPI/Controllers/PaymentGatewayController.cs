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
            BankRepository.AddTransaction(transaction);
            
            var vendorOrder = VendorRepository.Orders.FirstOrDefault(o => o.Id == transaction.Order_id);
            if (vendorOrder == null)
            {
                transaction.Status = TransactionStatus.Error;
                transaction.Message = "Заказа с таким номером не существует";
                return transaction.Message;
            }
            else vendorOrder.TransactionId = transaction.Id;

            var consumerCard = BankRepository.Cards.FirstOrDefault(c => (c.Card_number == transaction.Card_number &&
                                                                         c.Expiry_year == transaction.Expiry_year &&
                                                                         c.Expiry_month == transaction.Expiry_month &&
                                                                         c.Cvv == transaction.Cvv &&
                                                                         c.Cardholder_name == transaction.Cardholder_name));
            if (consumerCard == null)
            {
                transaction.Status = TransactionStatus.Error;
                transaction.Message = "Карты с такими данными не существует";
                return transaction.Message;
            }                

            if (DateTime.Now >= new DateTime(consumerCard.Expiry_year, consumerCard.Expiry_month, 1))
            {
                transaction.Status = TransactionStatus.Error;
                transaction.Message = "Истек срок действия карты";
                return transaction.Message;
            }

            if (consumerCard.Cash_limit != null && consumerCard.Cash_limit < transaction.Amount_kop)
            {
                transaction.Status = TransactionStatus.Error;
                transaction.Message = "Недостаточно средств на счету";
                return transaction.Message;
            }                

            if(consumerCard.Cash_limit != null)
            {
                consumerCard.Cash_limit -= transaction.Amount_kop;
            }
            VendorRepository.Cash += transaction.Amount_kop;
            transaction.Status = TransactionStatus.OK;
            transaction.Message = "Транзакция проведена успешно";
            return transaction.Message;
        }

        [HttpGet]
        public string GetStatus(int order_id)
        {
            var transaction = BankRepository.Transactions.FirstOrDefault(t => t.Order_id == order_id);
            if (transaction == null)
                return "Отсутствует транзакция на этот заказ";
            return transaction.Status.ToString();
        }

        [HttpDelete]
        public string Refund(int order_id)
        {
            var transaction = BankRepository.Transactions.FirstOrDefault(t => t.Order_id == order_id);
            if(transaction == null)
                return string.Format("Отсутствует транзакция на заказ {0}", order_id);

            var card = BankRepository.Cards.FirstOrDefault(c => c.Card_number == transaction.Card_number);

            if (transaction.Status != TransactionStatus.OK)
                return "Денежные средства по транзакции не переведены";

            VendorRepository.Cash -= transaction.Amount_kop;
            if (card.Cash_limit != null)
                card.Cash_limit += transaction.Amount_kop;

            transaction.Status = TransactionStatus.Сancel;
            transaction.Message = "Платеж возвращен";
            return string.Format("Произведен возврат средств по заказу {0} на карту {1}", order_id, card.Card_number);
        }

    }
}
