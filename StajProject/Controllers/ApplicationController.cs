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
        [Authorize]
        public ActionResult AssignManager()
        {
            //show it as Managers.ID  store it in Application.ID
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID");
            //show it as Managers.Name  store it in Managers.ID
            ViewBag.ManagerID = new SelectList(db.Managers, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignManager(Application_Manager application_manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Application_Manager.Add(application_manager);
                    db.SaveChanges();
                    return RedirectToAction("ViewApplicationList");
                }
                return View(application_manager);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AssignRecruiter()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID");
            ViewBag.RecruiterID = new SelectList(db.Recruiters, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignRecruiter(Application_Recruiter application_recruiter)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Application_Recruiter.Add(application_recruiter);
                    db.SaveChanges();
                    return RedirectToAction("ViewApplicationList");
                }
                return View(application_recruiter);
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        [HttpGet]
        public ActionResult AddApplication()
        {
            //in the textbox, FullName proprety is shows as a text but the selected cell is stored at ID
            //shows Candidates.Name as a list
            //store it in Candidates.ID
            ViewBag.CandidateID = new SelectList(db.Candidates, "ID", "Name");
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
