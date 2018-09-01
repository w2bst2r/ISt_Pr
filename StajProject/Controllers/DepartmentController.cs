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
            catch(Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            try
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
            catch (Exception)
            {
                throw;
            }
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
            catch(Exception)
            {
                throw;
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