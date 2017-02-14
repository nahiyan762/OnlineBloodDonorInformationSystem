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
    public class AccountController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            if ((string)Session["type"] != "member")
            {
                return RedirectToAction("Logout", "Home");
            }
            int id = Convert.ToInt32(Session["id"]);
            var result = db.Logins.Where(x => x.user_id == id).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "user_id,user_name,password,confirm_password,email,dob,contact,type")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.msg = "Profile Updated";
                //return RedirectToAction("Index");
            }
            return View(login);
        }

        [HttpGet]
        public ActionResult AccDelete(int? page)
        {
            if ((string)Session["type"] != "admin")
            {
                return RedirectToAction("Logout", "Home");
            }

            var list = db.Logins.Where(x=> x.type == "member");
            return View(list.ToList().ToPagedList(page??1,2));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Login result = db.Logins.Where(x => x.user_id == id).FirstOrDefault();
            return View(result);
        }


        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Login result = db.Logins.Where(x => x.user_id == id).FirstOrDefault();
            Donor result2 = db.Donors.Where(x => x.user_id == id).FirstOrDefault();
            string image = result2.donor_image;
            db.Donors.Remove(result2);
            db.Logins.Remove(result);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath("~/"+image));
            TempData["foo"] = "Member Deleted";
            return RedirectToAction("AccDelete");
        }

    }
}