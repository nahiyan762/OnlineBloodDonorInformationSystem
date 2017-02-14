using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BloodBankModels;
using PagedList;
using PagedList.Mvc;

namespace MvcBloodBankSystem.Controllers
{
    public class RenewController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();
        public ActionResult Index(int?page)
        {
            if ((string)Session["type"] != "admin")
            {
                return RedirectToAction("Logout", "Home");
            }

            var list = db.Donors.Where(x => x.status == "not available");
            return View(list.ToList().ToPagedList(page??1,2));
        }

        public ActionResult Confirm(int id)
        {
            Donor don = db.Donors.SingleOrDefault(x=>x.donor_id==id);
            don.status = "available";
            db.SaveChanges();
            TempData["mg"] = "Donor Renewed";
            return RedirectToAction("Index");
        }
    }
}