using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankModels;
using System.Data.Entity;
using System.Net;


namespace MvcBloodBankSystem.Controllers
{
    public class HomeController : Controller
    {
        private BloodBankDbContext db = new BloodBankDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult LoginForm(Login lo)
        {
            if(ModelState.IsValidField("email") && ModelState.IsValidField("paasword"))
            {
                var user = db.Logins.Where(x=>x.email == lo.email && x.password == lo.password).FirstOrDefault();
                if(user != null && user.type == "admin")
                {
                    Session["id"] = user.user_id;
                    Session["name"] = user.user_name;
                    Session["type"] = user.type;
                    return RedirectToAction("Index","WelcomeAdmin");
                }
                else if(user != null && user.type == "member")
                {
                    Session["id"] = user.user_id;
                    Session["name"] = user.user_name;
                    Session["type"] = user.type;
                    return RedirectToAction("Index", "WelcomeUser");
                }
                else
                {
                    ViewBag.msg = "Invalid UserName Or Password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["name"] = null;
            Session["type"] = null;
            return RedirectToAction("LoginForm");
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "user_id,user_name,password,confirm_password,email,dob,contact,type")] Login login)
        {
            if(db.Logins.Any(x=>x.email== login.email))
            {
                ViewBag.email=("Email Id Is Already Used");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //login.type = "member";
                    db.Logins.Add(login);
                    db.SaveChanges();
                    ViewBag.msg = "Registration Completed";
                }
            }

            return View(login);
        }
    }
}