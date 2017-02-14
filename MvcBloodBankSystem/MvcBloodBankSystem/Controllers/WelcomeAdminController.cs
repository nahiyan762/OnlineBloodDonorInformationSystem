using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankModels;
using System.Data.Entity;

namespace MvcBloodBankSystem.Controllers
{
    public class WelcomeAdminController : Controller
    {
        public ActionResult Index()
        {
            if((string)Session["type"] != "admin")
            {
                return RedirectToAction("Logout", "Home");
            }
            return View();
        }
    }
}