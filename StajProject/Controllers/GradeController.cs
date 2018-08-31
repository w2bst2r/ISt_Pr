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
    public class GradeController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewGradeList");
        }
        public ActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddGrade()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGrade(Grades grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Grades.Add(grade);
                    db.SaveChanges();
                    return RedirectToAction("ViewGradeList");
                }
                return View(grade);
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult DeleteGrade(int? id)
        {
            try
            {
                using (db)
                {
                    var grade = db.Grades.Where(x => x.ID == id).FirstOrDefault();
                    db.Grades.Remove(grade);
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
            return RedirectToAction("AddGrade");
        }

        [HttpGet]
        public ActionResult EditGrade(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        [HttpPost]
        public ActionResult EditGrade(Grades grade)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(grade).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(grade);
            }
            catch
            {
                return View();
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewGradeList()
        {
            return View(db.Grades.ToList());
        }

    }
}