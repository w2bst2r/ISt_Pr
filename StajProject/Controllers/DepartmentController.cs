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
    public class DepartmentController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: Candidate
        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewDepartmentList");
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Departments department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("ViewDepartmentList");
                }
                return View(department);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteDepartment(int? id)
        {
            try
            {
                using (db)
                {
                    var department = db.Departments.Where(x => x.ID == id).FirstOrDefault();
                    db.Departments.Remove(department);
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
            return RedirectToAction("AddDepartment");
        }

        [HttpGet]
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Departments department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(department).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(department);
            }
            catch
            {
                return View();
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewDepartmentList()
        {
            return View(db.Departments.ToList());
        }

        public ActionResult SubmitSurvey()
        {
            return View();
        }

    }
}