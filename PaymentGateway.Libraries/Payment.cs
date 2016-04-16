using PaymentGateway.Domain;
using PaymentGateway.Libraries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Libraries
{
    public class Payment : IPayment
    {
        string host;

        public Payment(string host)
        {
            this.host = host;
        }

        public async Task<string> Pay(int order_id, string card_number, byte expiry_month, short expiry_year, string cvv, string cardholder_name, decimal amount_kop)
        {
            using(var httpClient = new HttpClient())
            {
                SettingHttpClient(host, httpClient);

                var order = new Order
                {
                    Id = order_id,
                    Amount = amount_kop,
                    Payer = cardholder_name,
                    Paid = false
                };

                var response = await httpClient.PostAsJsonAsync("api/Pay", order);
                return response.StatusCode.ToString();
            }
            
        }

        public void GetStatus(int order_id)
        {

        }

        public void Refund(int order_id)
        {

        }

        private void SettingHttpClient(string host, HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(host);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
