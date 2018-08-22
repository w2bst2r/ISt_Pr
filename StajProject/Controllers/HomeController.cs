using StajProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StajProject.Controllers
{
    public class HomeController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Registrations registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var details = (from user in db.Registrations
                                   where user.Email == registration.Email && user.Password == registration.Password
                                   select new 
                                   {
                                       user.Email,
                                       user.Password
                                   }).ToList();

                    if (details.FirstOrDefault() != null)
                    {
                        Session["Email"] = details.FirstOrDefault().Email;
                        Session["Password"] = details.FirstOrDefault().Password;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credential");
                }
                return View("Login");
            }

            catch (Exception ex)
            {
                return View("Register");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Registrations registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
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
    }
}