using PaymentGateway.Domain;
using PaymentGateway.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateway.WebAPI.Controllers
{
    public class DataController : Controller
    {
        public ActionResult Index()
        {
            var dataViewModel = new DataViewModel
            {
                vendor_cash = VendorRepository.Cash,
                cards = BankRepository.Cards,
                transaction = BankRepository.Transactions
            };

            return View(dataViewModel);
        }
    }
}