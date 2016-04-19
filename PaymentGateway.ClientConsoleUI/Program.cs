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
            const decimal AMOUNT_KOP = 15000000m;

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

            // Рандомная карта для отлова ошибок
            //const string CARD_NUMBER = "0987654321234567";
            //const byte EXPIRY_MONTH = 3;
            //const short EXPIRY_YEAR = 2022;
            //const string CVV = "111";
            //const string CARDHOLDER_NAME = "";

            Console.WriteLine("Нажмите любую клавишу после полного запуска веб сервера...");
            Console.ReadKey();

            var payment = new Payment("http://localhost:33990/");  // Здесь указать свой локальный хост

            string answer = string.Empty;

            // Тестирует метод Pay(...)
            while(true)
            {
                Console.WriteLine("Производим трансферт на сумму {0} ...", AMOUNT_KOP);
                answer = await payment.Pay(ORDER_NUMBER,
                                           CARD_NUMBER,
                                           EXPIRY_MONTH,
                                           EXPIRY_YEAR,
                                           CVV,
                                           CARDHOLDER_NAME,
                                           AMOUNT_KOP);

                Console.WriteLine("Результат операции: {0}", answer);
                Console.WriteLine();

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
            
            // Тестируем метод GetStatus(...)
            for (int i = 1; i <= 5; i++)
            {
                answer = await payment.GetStatus(i);
                Console.WriteLine("Статус заказа {0}: {1}", i, answer);
            }
            Console.ReadKey();

            // Тестируем метод Refund(...)
            for (int i = 1; i <= 3; i++)
            {
                answer = await payment.Refund(i);
                Console.WriteLine("Возврат платежа заказа {0}: {1}", i, answer);
            }
            Console.ReadKey();
        }
    }
}
