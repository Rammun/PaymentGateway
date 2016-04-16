using PaymentGateway.Domain;
using PaymentGateway.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.ClientConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var payment = new Payment("http://localhost:33990/");

            payment.Pay(111, "123123", (byte)1, (short)2017, "500", "Ruslan", (decimal)1000000);
        }

        //static void Main()
        //{
        //    RunAsync().Wait();
        //}

        //static async Task RunAsync()
        //{
            
        //}
    }
}
