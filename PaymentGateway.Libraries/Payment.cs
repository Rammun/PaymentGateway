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
                    Card_number = cardholder_name,
                    Expiry_month = expiry_month,
                    Expiry_year = expiry_year,
                    Cvv = cvv,
                    Cardholder_name = cardholder_name,
                    Amount_kop = amount_kop,
                    Date = DateTime.Now,
                    Status = TransactionStatus.OK
                };

                var response = await httpClient.PostAsJsonAsync("api/Pay", transaction);
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
