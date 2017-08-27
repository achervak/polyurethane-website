using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.Communication;

namespace polyurethane_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailCommunication _emailCommunication;

        public HomeController(IEmailCommunication emailCommunication)
        {
            _emailCommunication = emailCommunication;

        }

        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult DeliveryDetails()
        {
            return View();
        }
        
        

        public ActionResult Basket()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult AboutUs() {
            return View();
        }

        public void SendQuickMail(string email, string name, string message)
        {
            _emailCommunication.SendEmail("andrii.chervak91@gmail.com", "quick feedback", message);
        }
    }
}