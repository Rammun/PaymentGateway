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

        public async Task<string> Pay(int order_id,
                                      string card_number,
                                      byte expiry_month,
                                      short expiry_year,
                                      string cvv,
                                      string cardholder_name,
                                      decimal amount_kop)
        {
            using(var httpClient = new HttpClient())
            {
                SettingHttpClient(host, httpClient);

                var transaction = new Transaction()
                {
                    Order_id = order_id,
                    Card_number = card_number,
                    Expiry_month = expiry_month,
                    Expiry_year = expiry_year,
                    Cvv = cvv,
                    Cardholder_name = cardholder_name,
                    Amount_kop = amount_kop,
                    Date = DateTime.Now,
                    Status = TransactionStatus.Empty
                };

                var response = httpClient.PostAsJsonAsync("api/PaymentGateway", transaction).Result;
                var answer = await response.Content.ReadAsAsync<string>();
                return answer;
            }            
        }

        public async Task<string> GetStatus(int order_id)
        {
            using (var httpClient = new HttpClient())
            {
                SettingHttpClient(host, httpClient);

                var response = await httpClient.GetAsync(string.Format(@"api/PaymentGateway/{0}", order_id));
                return await response.Content.ReadAsAsync<string>();
            }
        }

        public async Task<string> Refund(int order_id)
        {
            using (var httpClient = new HttpClient())
            {
                SettingHttpClient(host, httpClient);

                var response = await httpClient.PutAsJsonAsync("api/PaymentGateway", order_id);
                return await response.Content.ReadAsAsync<string>();
            }
        }

        private void SettingHttpClient(string host, HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(host);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
