using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kermesse.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            /*
            if (Session["UserID"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            */
            return View();
        }
    }
}