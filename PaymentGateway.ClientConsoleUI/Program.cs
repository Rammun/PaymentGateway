using PaymentGateway.Domain;
using PaymentGateway.Libraries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.ClientConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            const int ORDER_NUMBER = 1;
            const decimal AMOUNT_KOP = 20000000m;

            // Безлимитная карта
            //const string CARD_NUMBER = "1234567890987654";
            //const byte EXPIRY_MONTH = 4;
            //const short EXPIRY_YEAR = 2023;
            //const string CVV = "002";
            //const string CARDHOLDER_NAME = "Ktoto";

            // Карта с лимитом 30000000
            const string CARD_NUMBER = "0987654321234567";
            const byte EXPIRY_MONTH = 3;
            const short EXPIRY_YEAR = 2022;
            const string CVV = "111";
            const string CARDHOLDER_NAME = "";

            Console.WriteLine("Нажмите любую клавишу после полного запуска веб сервера...");
            Console.ReadKey();

            var payment = new Payment("http://localhost:33990/");  // Здесь указать свой локальный хост

            while(true)
            {
                var card = BankRepository.Cards.FirstOrDefault(c => c.Card_number == CARD_NUMBER);
                string cash;
                if (card.Cash_limit == null)
                    cash = "безлимит";
                else cash = card.Cash_limit.ToString();

                Console.WriteLine("Производим трансфер на сумму {0} ...", AMOUNT_KOP);

                var answer = await payment.Pay(ORDER_NUMBER,
                                               CARD_NUMBER,
                                               EXPIRY_MONTH,
                                               EXPIRY_YEAR,
                                               CVV,
                                               CARDHOLDER_NAME,
                                               AMOUNT_KOP);

                Console.WriteLine("Результат операции: {0}", answer);
                Console.WriteLine();

                Console.ReadKey();
            }            
        }
    }
}
