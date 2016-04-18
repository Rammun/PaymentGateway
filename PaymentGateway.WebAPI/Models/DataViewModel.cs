using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace PaymentGateway.WebAPI.Models
{
    public class DataViewModel
    {
        public decimal vendor_cash;
        public ReadOnlyCollection<Card> cards;
        public ReadOnlyCollection<Transaction> transaction;
    }
}