using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class Transaction
    {
        private static int count;

        public Transaction()
        {
            this.Id = ++count;
            this.Date = DateTime.Now;
            this.Status = TransactionStatus.Empty;
            this.Message = string.Empty;
        }
        
        public int Id { get; private set; }
        public int Order_id { get; set; }
        public string Card_number { get; set; }
        public byte Expiry_month { get; set; }
        public short Expiry_year { get; set; }
        public string Cvv { get; set; }
        public string Cardholder_name { get; set; }
        public decimal Amount_kop { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatus Status { get; set; }
        public string Message { get; set; }
    }
}
