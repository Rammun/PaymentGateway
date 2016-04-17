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
        static List<string> list = new List<string>();
        static ReadOnlyCollection<string> readOnlyList = new ReadOnlyCollection<string>(list);

        static void Main(string[] args)
        {
            list = new List<string>();
            readOnlyList = new ReadOnlyCollection<string>(list);
            list.Add("rus");
            list.Add("aaa");
            list.Add("bbb");
            foreach (var item in readOnlyList)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        //static void Main()
        //{
        //    RunAsync().Wait();
        //}

        //static async Task RunAsync()
        //{
        //    var payment = new Payment("http://localhost:33990/");

        //    var answer = await payment.Pay(111, "123123", (byte)1, (short)2017, "500", "Ruslan", (decimal)1000000);
        //    Console.WriteLine(answer);
        //    Console.ReadKey();
        //}
    }
}
