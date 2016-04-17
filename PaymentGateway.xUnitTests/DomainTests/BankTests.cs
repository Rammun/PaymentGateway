using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.xUnitTests.DomainTests
{
    public class BankTests
    {
        [Fact]
        public void BankDefault()
        {
            var count = Bank.Cards.Count;

            Assert.Equal(count, 5);
        }

        [Fact]
        public void Bank_AddCard_Count()
        {
            var countBefore = Bank.Cards.Count;
            Bank.AddCard(new Card("1324354657687980", 1, 2020, "001", "R"));
            var countAfter = Bank.Cards.Count;

            Assert.Equal(countAfter - countBefore, 1);
        }

        [Fact]
        public void Bank_DeleteCard_Count()
        {
            var countBefore = Bank.Cards.Count;
            Bank.DeleteCard("1234567890987654");
            var countAfter = Bank.Cards.Count;

            Assert.Equal(countBefore - countAfter, 1);
        }

        [Fact]
        public void Bank_DeleteCard_NonExistentCard()
        {
            Action method = () => { Bank.DeleteCard("1111111111111111"); };

            var ex = Record.Exception(method);

            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }

        [Fact]
        public void Bank_AddTransaction_Count()
        {
            var countBefore = Bank.Transactions.Count;
            Bank.AddTransaction(new Transaction
                                {
                                    Order_id = 1,
                                    Card_number = "1234567890987654",
                                    Expiry_month = 4,
                                    Expiry_year = 2023,
                                    Cvv = "002",
                                    Cardholder_name = "Ktoto",
                                    Amount_kop = 10000,
                                    Date = DateTime.Now
                                });
            var countAfter = Bank.Transactions.Count;

            Assert.Equal(countAfter - countBefore, 1);
        }

        [Fact]
        public void Bank_DeleteTransaction_Count()
        {
            Bank.AddTransaction(new Transaction
            {
                Order_id = 1,
                Card_number = "1234567890987654",
                Expiry_month = 4,
                Expiry_year = 2023,
                Cvv = "002",
                Cardholder_name = "Ktoto",
                Amount_kop = 10000,
                Date = DateTime.Now
            });
            var countBefore = Bank.Transactions.Count;
            Bank.DeleteTransaction(1);
            var countAfter = Bank.Transactions.Count;

            Assert.Equal(countBefore - countAfter, 1);
        }

        [Fact]
        public void Bank_DeleteTransaction_NonExistentCard()
        {
            Action method = () => { Bank.DeleteCard("1111111111111111"); };

            var ex = Record.Exception(method);

            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }
    }
}
