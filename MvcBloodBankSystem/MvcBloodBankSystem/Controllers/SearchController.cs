using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBankModels;
using PagedList;
using PagedList.Mvc;

namespace MvcBloodBankSystem.Controllers
{
    public class SearchController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();

        [HttpGet]
        public ActionResult Index(int ? page)
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }

            ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
            ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
            var donors = db.Donors.Include(d => d.AreaId).Include(d => d.BloodId).Include(d => d.CityId).Where(x => x.status == "available");
            return View(donors.ToList().ToPagedList(page ??1,2));
        }
        
        public ActionResult Search(string blood_id,string city_id,string area_id,int?page,string blood,string city,string area)
        {
            if(blood != string.Empty && city == string.Empty && area == string.Empty)
            {
                Report(blood,"--","--");
            }
            else if(blood != string.Empty && city != string.Empty && area == string.Empty)
            {
                Report(blood,city, "--");
            }
            else if(blood != string.Empty && city != string.Empty && area != string.Empty)
            {
                Report(blood, city, area);
            }
            else if(blood == string.Empty && city == string.Empty && area == string.Empty)
            {
                TempData["war"] = "Selection Of Blood Group Is Mandatory";
                return RedirectToAction("Index");
            }

            if (blood_id != string.Empty && city_id == string.Empty && area_id == string.Empty)
            {
                ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
                ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
                
                int id = Convert.ToInt32(blood_id);
                var don = db.Donors.Include(d => d.AreaId).Include(d => d.BloodId).Include(d => d.CityId).Where(x => x.status == "available" && x.blood_id == id);
                return View("Index",don.ToList().ToPagedList(page ?? 1, 2));
            }
            else if(blood_id != string.Empty && city_id != string.Empty && area_id == string.Empty)
            {
                ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
                ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
                
                int id = Convert.ToInt32(blood_id);
                int id2 = Convert.ToInt32(city_id);
                var don = db.Donors.Include(d => d.AreaId).Include(d => d.BloodId).Include(d => d.CityId).Where(x => x.status == "available" && x.blood_id == id && x.city_id == id2);
                return View("Index",don.ToList().ToPagedList(page ?? 1, 2));
            }
            else if(blood_id != string.Empty && city_id != string.Empty && area_id != string.Empty)
            {
                ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
                ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
                
                int id = Convert.ToInt32(blood_id);
                int id2 = Convert.ToInt32(city_id);
                int id3 = Convert.ToInt32(area_id);
                var don = db.Donors.Include(d => d.AreaId).Include(d => d.BloodId).Include(d => d.CityId).Where(x => x.status == "available" && x.blood_id == id && x.city_id == id2 && x.area_id == id3);
                return View("Index",don.ToList().ToPagedList(page ?? 1, 2));
            }
            else
            {
                TempData["war"] = "Selection Of Blood Group Is Mandatory";
                return RedirectToAction("Index");
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCity(string city_id)
        {
            int id = Convert.ToInt16(city_id);
            var list = db.Areas.Where(x => x.city_id == id);
            var classesData = list.Select(m => new SelectListItem()
            {
                Text = m.area_name,
                Value = m.area_id.ToString(),
            });
            return Json(classesData, JsonRequestBehavior.AllowGet);
        }

        public void Report(string blood,string city,string area)
        {
            ReportSearch rs = new ReportSearch();
            rs.blood = blood;
            rs.city = city;
            rs.area = area;
            rs.date_now = DateTime.Now;
            db.Reports.Add(rs);
            db.SaveChanges();
        }
    }
}
