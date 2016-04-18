using Moq;
using PaymentGateway.Domain;
using PaymentGateway.WebAPI.Controllers;
using System;
using System.Linq;
using Xunit;

namespace PaymentGateway.xUnitTests.WebApiTests
{
    public class PaymentGatewayControllerTest
    {
        [Theory]
        [InlineData(9, "1029384756473829", 5, 2024, "022", "This is I", 1000)]     // Несуществующий номер заказа
        [InlineData(1, "0029384756473829", 5, 2024, "022", "This is I", 1000)]     // Несуществующий номер карты
        [InlineData(1, "1029384756473829", 5, 2000, "022", "This is I", 1000)]     // Истекло время действия карты
        [InlineData(1, "1029384756473829", 5, 2024, "555", "This is I", 1000)]     // Неверный дополнительный код
        [InlineData(1, "1029384756473829", 5, 2024, "022", "This", 1000)]          // Неверное имя владельца карты
        [InlineData(1, "1029384756473829", 5, 2024, "022", "This is I", 5000000)]  // Недостаточно средств на счету для оплаты
        public void Pay_Transaction_Erorr(int order_id,
                                          string card_number,
                                          byte expiry_month,
                                          short expiry_year,
                                          string cvv,
                                          string cardholder_name,
                                          decimal amount_kop)
        {
            var paymentGatewayController = new PaymentGatewayController();
            var transaction = new Transaction()
            {
                Order_id = order_id,
                Card_number = card_number,
                Expiry_month = expiry_month,
                Expiry_year = expiry_year,
                Cvv = cvv,
                Cardholder_name = cardholder_name,
                Amount_kop = amount_kop
            };

            var answer = paymentGatewayController.Pay(transaction);

            Assert.Equal(transaction.Status, TransactionStatus.Error);
        }

        [Theory]
        [InlineData(1, "1029384756473829", 5, 2024, "022", "This is I", 10000)] // Карта с лимитом денежных средств
        [InlineData(1, "1234567890123456", 1, 2020, "001", "R", 10000)]         // Карта без лимита денежных средств
        public void Pay_Transaction_Success(int order_id,
                                            string card_number,
                                            byte expiry_month,
                                            short expiry_year,
                                            string cvv,
                                            string cardholder_name,
                                            decimal amount_kop)
        {
            var paymentGatewayController = new PaymentGatewayController();
            var transaction = new Transaction()
            {
                Order_id = order_id,
                Card_number = card_number,
                Expiry_month = expiry_month,
                Expiry_year = expiry_year,
                Cvv = cvv,
                Cardholder_name = cardholder_name,
                Amount_kop = amount_kop
            };

            // Запоминаем состояние счетов до перевода денег
            var card = BankRepository.Cards.FirstOrDefault(c => c.Card_number == card_number);
            var cardCashBefore = card.Cash_limit;
            var vendorCashBefore = VendorRepository.Cash;
            
            // Переводим деньги
            var answer = paymentGatewayController.Pay(transaction);

            // Запоминаем состояние счетов после перевода денег
            var cardCashAfter = card.Cash_limit;
            var vendorCashAfter = VendorRepository.Cash;

            var cardDifference = cardCashBefore - cardCashAfter;
            var vendorDifference = vendorCashAfter - vendorCashBefore;

            Assert.Equal(transaction.Status, TransactionStatus.OK);
            Assert.Equal(vendorDifference, transaction.Amount_kop);
            if (cardCashBefore == null)   // Счет безлимитной карточки не проверяем
                return;
            Assert.Equal(cardDifference, transaction.Amount_kop);
            Assert.Equal(cardDifference, vendorDifference);
        }        

        [Theory]
        [InlineData(1, TransactionStatus.Empty)]
        [InlineData(2, TransactionStatus.Error)]
        [InlineData(3, TransactionStatus.OK)]
        public void GetStatus_TransactionStatus_EqualStatus(int order_id, TransactionStatus status)
        {
            var paymentGatewayController = new PaymentGatewayController();
                        
            var transaction = new Transaction()
            {
                Order_id = order_id,
                Status = status
            };
            BankRepository.AddTransaction(transaction);

            var answer = paymentGatewayController.GetStatus(order_id);

            Assert.Equal(transaction.Status.ToString(), answer);
        }

        [Fact]
        public void Refund_OrderId_TransactionStatusСancel()
        {
            var paymentGatewayController = new PaymentGatewayController();

            var transaction = new Transaction()
            {
                Order_id = 4,
                Card_number = "1029384756473829",
                Expiry_month = 5,
                Expiry_year = 2024,
                Cvv = "022",
                Cardholder_name = "This is I",
                Amount_kop = 10000,
                Status = TransactionStatus.OK
            };
            BankRepository.AddTransaction(transaction);
            var card = BankRepository.Cards.FirstOrDefault(c => c.Card_number == transaction.Card_number);

            var cardCashBefore = card.Cash_limit;
            var vendorCashBefore = VendorRepository.Cash;

            var answer = paymentGatewayController.Refund(4);

            var cardCashAfter = card.Cash_limit;
            var vendorCashAfter = VendorRepository.Cash;

            var cardDifference = cardCashAfter - cardCashBefore;
            var vendorDifference = vendorCashBefore - vendorCashAfter;

            Assert.Equal(transaction.Status, TransactionStatus.Сancel);
            Assert.Equal(cardDifference, vendorDifference);
            Assert.Equal(answer, "Произведен возврат средств по заказу 4");
        }

        [Fact]
        public void Refund_OrderId_OrderFail()
        {
            var paymentGatewayController = new PaymentGatewayController();

            var answer = paymentGatewayController.Refund(10);

            Assert.Equal(answer, "Отсутствует транзакция на заказ 10");
        }

        [Fact]
        public void Refund_OrderId_StatusFail()
        {
            var paymentGatewayController = new PaymentGatewayController();
            BankRepository.AddTransaction(new Transaction { Order_id = 11, Status = TransactionStatus.Error });

            var answer = paymentGatewayController.Refund(11);

            Assert.Equal(answer, "Денежные средства по транзакции не переведены");
        }
    }
}
