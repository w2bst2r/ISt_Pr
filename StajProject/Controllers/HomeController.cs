using StajProject.Filters;
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

        public ActionResult Login(string previousURL)
        {
            Session["previousURL"] = previousURL;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Registrations registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var details = (from user in db.Registrations //for each row in Registrations table
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
                        if (Session["previousURL"] == null)
                        {
                            return RedirectToAction("Index");
                        }
                        else return Redirect(Session["previousURL"].ToString());
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Credentials. Please verify your Email and Password");
                    }
                }
                return View("Login");
            }

            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Logout()
        {
            Session["Email"] = null;
            Session["Password"] = null;
            return RedirectToAction("Index");
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
            catch (Exception)
            {
                throw;
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