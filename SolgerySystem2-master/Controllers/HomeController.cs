using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolgerySystem2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult index1()
        {
            //Session.Add("username",User.Identity.Name);
            if (UserVerified())
            {
                return RedirectToAction("Index", "Groups");
            }
            return RedirectToAction("Login", "Account");

        }
        private bool UserVerified()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        }
    }
}