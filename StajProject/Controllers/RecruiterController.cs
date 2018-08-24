using StajProject.Filters;
using StajProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StajProject.Controllers
{
    [AdminFilter]
    public class RecruiterController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewRecruiterList");
        }
        public ActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddRecruiter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRecruiter(Recruiters recruiter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Recruiters.Add(recruiter);
                    db.SaveChanges();
                    return RedirectToAction("ViewRecruiterList");
                }
                return View(recruiter);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteRecruiter(int? id)
        {
            try
            {
                using (db)
                {
                    var recruiter = db.Recruiters.Where(x => x.ID == id).FirstOrDefault();
                    db.Recruiters.Remove(recruiter);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ClearForm()
        {
            ModelState.Clear();
            return RedirectToAction("AddRecruiter");
        }

        [HttpGet]
        public ActionResult EditRecruiter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        [HttpPost]
        public ActionResult EditRecruiter(Recruiters recruiter)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(recruiter).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(recruiter);
            }
            catch
            {
                return View();
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewRecruiterList()
        {
            return View(db.Recruiters.ToList());
        }

    }
}