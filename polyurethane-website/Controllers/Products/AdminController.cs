using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace polyurethane_website.Controllers.Products
{
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        [Route("login")]
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            return Redirect("/admin/home");
        }

        [Route("home")]
        [HttpGet]
        public ViewResult AdminHome()
        {
            return View();
        }
    }
}