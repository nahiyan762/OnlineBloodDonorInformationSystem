using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBloodBankSystem.Controllers
{
    public class WelcomeUserController : Controller
    {
        // GET: WelcomeUser
        public ActionResult Index()
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }
            return View();
        }
    }
}