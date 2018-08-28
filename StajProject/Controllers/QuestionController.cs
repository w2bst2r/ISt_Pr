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
    public class QuestionController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewQuestionList");
        }
        public ActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(Questions question_parameter_)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Questions.Add(question_parameter_);
                    db.SaveChanges();
                    return RedirectToAction("ViewQuestionList");
                }
                return View(question_parameter_);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteQuestion(int? id)
        {
            try
            {
                using (db)
                {
                    var question = db.Questions.Where(x => x.ID == id).FirstOrDefault();
                    db.Questions.Remove(question);
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
            return RedirectToAction("AddQuestion");
        }

        [HttpGet]
        public ActionResult EditQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [HttpPost]
        public ActionResult EditQuestion(Questions question_parameter_)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(question_parameter_).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(question_parameter_);
            }
            catch
            {
                return View();
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewQuestionList()
        {
            return View(db.Questions.ToList());
        }
    }
}