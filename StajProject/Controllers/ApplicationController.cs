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
    public class ApplicationController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        public ActionResult Index()
        {
            return RedirectToAction("ViewApplicationList");
        }
        public ActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AssignManager()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID");
            ViewBag.ManagerID = new SelectList(db.Managers, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignManager(Applications application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Applications.Add(application);
                    db.SaveChanges();
                    return RedirectToAction("ViewApplicationList");
                }
                return View(application);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AssignRecruiter()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddApplication()
        {
            ViewBag.CandidateID = new SelectList(db.Candidates, "ID", "FullName");
            //ViewBag.CandidateSurname = new SelectList(db.Candidates, "ID", "SurName");
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeID = new SelectList(db.Grades, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddApplication(Applications application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Applications.Add(application);
                    db.SaveChanges();
                    return RedirectToAction("ViewApplicationList");
                }
                return View(application);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteApplication(int? id)
        {
            try
            {
                using (db)
                {
                    var application = db.Applications.Where(x => x.ID == id).FirstOrDefault();
                    db.Applications.Remove(application);
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
            return View("AddApplication");
        }

        [HttpGet]
        public ActionResult EditApplication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "ID", "Name");
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeID = new SelectList(db.Grades, "ID", "Name");

            var application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        [HttpPost]
        public ActionResult EditApplication(Applications application)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(application);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewApplicationList()
        {
            return View(db.Applications.ToList());
        }

    }
}
