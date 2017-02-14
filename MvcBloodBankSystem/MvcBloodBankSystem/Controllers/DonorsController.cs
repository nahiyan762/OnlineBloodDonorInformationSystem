using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBankModels;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace MvcBloodBankSystem.Controllers
{
    public class DonorsController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();
        // GET: Donors
        [HttpGet]
        public ActionResult Index()
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }

            ViewBag.blood_id = new SelectList(db.Bloods,"blood_id","blood_name");
            ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "donor_name,donor_number,donor_image,donor_age,donor_gender,blood_id,city_id,area_id,start_date,end_date,comments,status")]Donor don, HttpPostedFileBase donor_image)
        {
            //Donor don = new Donor();
            int ido = Convert.ToInt32(Session["id"]);
            if (db.Donors.Any(x => x.user_id == ido))
            {
                ViewBag.al = ("You Are Already Registered As A Donor");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    don.user_id = ido;
                    don.donor_image = "Image/"+ donor_image.FileName.ToString();
                   

                    db.Donors.Add(don);
                    db.SaveChanges();
                    donor_image.SaveAs(Server.MapPath("~/Image/"+donor_image.FileName.ToString()));
                    ViewBag.alia = ("Registeration Done Successfully");
                }
            }
            ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
            ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
            return View();
        }

        [HttpGet]      
        public ActionResult Edit()
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }
            int ido = Convert.ToInt32(Session["id"]);
            var don = db.Donors.Where(x=>x.user_id==ido).FirstOrDefault();
            ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
            ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name");
            return View(don);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "donor_id,donor_name,donor_number,donor_image,donor_age,donor_gender,blood_id,city_id,area_id,start_date,end_date,comments,status")]Donor donor, HttpPostedFileBase donor_image)
        {
            if (ModelState.IsValid)
            {
                if(donor_image == null || donor_image.ContentLength == 0)
                {
                    donor.user_id = Convert.ToInt32(Session["id"]);
                    ViewBag.msg = "Profile Updated";
                    db.Entry(donor).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    string image = donor.donor_image;
                    donor.user_id = Convert.ToInt32(Session["id"]);
                    donor.donor_image = "Image/" + donor_image.FileName.ToString();
                    ViewBag.msg = "Profile Updated";
                    db.Entry(donor).State = EntityState.Modified;
                    db.SaveChanges();
                    System.IO.File.Delete(Server.MapPath("~/"+image));
                    donor_image.SaveAs(Server.MapPath("~/Image/" + donor_image.FileName.ToString()));
                }
            }
            ViewBag.blood_id = new SelectList(db.Bloods, "blood_id", "blood_name");
            ViewBag.city_id = new SelectList(db.Cities, "city_id", "city_name");
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name");
            return View(donor);
        }

        public ActionResult Comment()
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }
            int ido = Convert.ToInt32(Session["id"]);
            var don = db.Donors.Where(x => x.user_id == ido).FirstOrDefault();
            return View(don);
        }

        [HttpPost]
        public ActionResult Comment(Donor d)
        {
            if(ModelState.IsValidField("start_date") && ModelState.IsValidField("comments") && ModelState.IsValidField("status"))
            {
                Donor don = new Donor();
                int id = Convert.ToInt32(Request["donor_id"]);
                don = db.Donors.SingleOrDefault(x=>x.donor_id==id);
                don.start_date = d.start_date;
                don.end_date = d.start_date.AddDays(90);
                don.comments = d.comments;
                don.status = d.status;
                db.SaveChanges();
                ViewBag.msg = "Comment Posted";
            }
            return View(d);
        }
        public ActionResult Delete()
        {
            int user_id = Convert.ToInt32(Session["id"]);
            Donor result = db.Donors.Where(x=>x.user_id==user_id).FirstOrDefault();
            string image = result.donor_image;
            db.Donors.Remove(result);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath("~/"+image));
            return RedirectToAction("Edit");
        }

        public ActionResult Report(int?page)
        {
            if ((string)Session["type"] != "admin")
            {
                return RedirectToAction("Logout", "Home");
            }
            var result = db.Donors.Include(db=>db.BloodId).ToList();
            return View(result.ToPagedList(page ?? 1,2));
        }

        public ActionResult Search(string start_date,string end_date,int?page)
        {
            if(start_date != string.Empty && end_date != string.Empty)
            {
                DateTime d1 = DateTime.ParseExact(start_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime d2 = DateTime.ParseExact(end_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                int result = DateTime.Compare(d1, d2);
                if (result > 0)
                {
                    TempData["mg"] = "Start Date Must Not Be Later On End Date";
                    return RedirectToAction("Report");
                }
                else
                {
                    var result2 = from d in db.Donors where (d.start_date >= d1 && d.start_date <= d2) select d;
                    return View("Report", result2.Include(d=>d.BloodId).ToList().ToPagedList(page??1,2));
                }
            }
            else
            {
                return RedirectToAction("Report");
            }
        }
    }
}