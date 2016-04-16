using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class CardModel
    {
        public string Card_number { get; set; }
        public byte Expiry_month { get; set; }
        public short Expiry_year { get; set; }
        public string Cvv { get; set; }
        public string Cardholder_name { get; set; }
    }
}
