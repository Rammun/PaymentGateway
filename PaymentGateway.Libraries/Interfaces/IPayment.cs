using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Libraries.Interfaces
{
    public interface IPayment
    {
        Task<string> Pay(int order_id, string card_number, byte expiry_month, short expiry_year, string cvv, string cardholder_name, decimal amount_kop);

        Task<string> GetStatus(int order_id);

        Task<string> Refund(int order_id);
    }
}
