using PaymentGateway.Domain;
using PaymentGateway.WebAPI.Controllers;
using System;
using System.Linq;
using Xunit;

namespace PaymentGateway.xUnitTests.WebApi
{
    public class PaymentGatewayControllerTest
    {
        [Fact]
        public void Pay()
        {
            var paymentGatewayController = new PaymentGatewayController();
            var transaction = new Transaction()
            {
                Order_id = 1,
                Card_number = "1234567890987654",
                Expiry_month = 4,
                Expiry_year = 2023,
                Cvv = "002",
                Cardholder_name = "Ktoto",
                Amount_kop = 1000,
                Date = DateTime.Now,
                Status = TransactionStatus.OK
            };

            var answer = paymentGatewayController.Pay(transaction);

            Assert.Equal(answer, "Транзакция проведена успешно");

        }
    }
}
