using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace polyurethane_website.Controllers
{
    public class MainController : Controller
    {
        // GET: Default
        public ActionResult Main()
        {
            ViewBag.Name = "Gogi";
            return View();
        }
    }
}