using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace polyurethane_website.Controllers
{
    public class HomeController : Controller
    {       
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
        
        public ActionResult Order()
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
    }
}