using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.xUnitTests.DomainTests
{
    public class CardTests
    {
        [Fact]
        public void CreateCardIsSuccess()
        {
            string card_number = "1234567890987654";
            byte expiry_month = 1;
            short expiry_year = 2020;
            string cvv = "500";
            string cardholder_name = "R";
            decimal? cash_limit = 1000000;

            Action method = () => { new Card(card_number, expiry_month, expiry_year, cvv, cardholder_name, cash_limit); };

            var ex = Record.Exception(method);
            Assert.Null(ex);
        }

        [Theory]
        [InlineData("1234", 2, 2020, "222", "", null)]                // Длина номера карты меньше 16 цифр
        [InlineData("1234567890p98765", 2, 2020, "222", "", null)]    // В номере карты есть не цифровые символы
        [InlineData("1234567890987654123", 2, 2020, "222", "", null)] // Номер карты длинее 16
        [InlineData("1234567890987654", 0, 2020, "222", "", null)]    // Недопустимый номер месяца
        [InlineData("1234567890987654", 2, 2000, "222", "", null)]    // Год истечения карты меньше текущего
        [InlineData("1234567890987654", 2, 2020, "2", "", null)]      // Дополнительный код меньше 3 цифр
        [InlineData("1234567890987654", 2, 2020, "2222", "", null)]   // Дополнительный код больше 3 цифр
        [InlineData("1234567890987654", 2, 2020, "22а", "", null)]    // Дополнительный код имеет нецифровой символ
        [InlineData("1234567890987654", 2, 2020, "222", null, null)]  // Поле с именем владельца карты равно null
        public void CreateCardIsFale(string card_number,
                                     byte expiry_month,
                                     short expiry_year,
                                     string cvv,
                                     string cardholder_name,
                                     decimal? cash_limit)
        {
            Action method = () => { new Card(card_number, expiry_month, expiry_year, cvv, cardholder_name, cash_limit); };

            var ex = Record.Exception(method);

            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }

        [Fact]
        public void Card_IsEquals_True()
        {
            var card1 = new Card("1234567890987654", 1, 2020, "500");
            var card2 = new Card("1234567890987654", 1, 2020, "500", "", 200000);

            Assert.True(card1.IsEquals(card2));
        }

        [Fact]
        public void Card_IsEquals_False()
        {
            var card1 = new Card("1234567890987654", 1, 2020, "500");
            var card2 = new Card("1234567890987654", 1, 2020, "500", "R");

            Assert.False(card1.IsEquals(card2));
        }

        [Theory]
        [InlineData("0234567890987654", 1, 2020, "500", "", null)]   // Отличается номер карты
        [InlineData("1234567890987654", 2, 2020, "500", "", null)]   // Отличается месяц
        [InlineData("1234567890987654", 1, 2030, "500", "", null)]   // Отличается год
        [InlineData("1234567890987654", 1, 2020, "005", "", null)]   // Отличается дополнительный код
        [InlineData("1234567890987654", 1, 2020, "500", "R", null)]  // Отличается имя владельца карты
        public void CardsAreNotEqual(string card_number,
                                     byte expiry_month,
                                     short expiry_year,
                                     string cvv,
                                     string cardholder_name,
                                     decimal? cash_limit)
        {
            var card1 = new Card("1234567890987654", 1, 2020, "500");
            Card card2;

            card2 = new Card(card_number, expiry_month, expiry_year, cvv, cardholder_name, cash_limit);
            Assert.NotEqual(card1, card2);
        }
    }
}
