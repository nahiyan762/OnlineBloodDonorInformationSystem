using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankModels;
using PagedList;
using PagedList.Mvc;

namespace MvcBloodBankSystem.Controllers
{
    public class ReportController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();

        public ActionResult Index(int?page)
        {
            if ((string)Session["type"] != "admin")
            {
                return RedirectToAction("Logout", "Home");
            }
            var result = db.Reports.OrderBy(x => x.blood).ToList();
            return View(result.ToPagedList(page??1,6));
        }

        public ActionResult SearchReport(string start_date,string end_date,int?page)
        {
            if(start_date != string.Empty && end_date != string.Empty)
            {
                DateTime d1 = DateTime.ParseExact(start_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime d2 = DateTime.ParseExact(end_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                int result = DateTime.Compare(d1,d2);
                if(result > 0)
                {
                    TempData["mg"]="Start Date Must Not Be Later On End Date";
                    return RedirectToAction("Index");
                }
                else
                {
                    var result2 = from d in db.Reports where (d.date_now >= d1 && d.date_now <= d2) orderby d.blood select d;
                    return View("Index", result2.ToPagedList(page??1,6));
                } 
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}