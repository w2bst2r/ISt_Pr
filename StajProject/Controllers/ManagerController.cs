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
    public class ManagerController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: Candidate
        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewManagerList");
        }

        [HttpGet]
        public ActionResult AddManager()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddManager(Managers manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Managers.Add(manager);
                    db.SaveChanges();
                    return RedirectToAction("ViewManagerList");
                }
                return View(manager);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteManager(int? id)
        {
            try
            {
                using (db)
                {
                    var manager = db.Managers.Where(x => x.ID == id).FirstOrDefault();
                    db.Managers.Remove(manager);
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
            return RedirectToAction("AddManager");
        }

        [HttpGet]
        public ActionResult EditManager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        [HttpPost]
        public ActionResult EditManager(Managers manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(manager).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(manager);
            }
            catch
            {
                return View();
            }
        }
        [OverrideActionFilters]
        public ActionResult ViewManagerList()
        {
            return View(db.Managers.ToList());
        }

        public ActionResult SubmitSurvey()
        {
            return View();
        }

    }
}