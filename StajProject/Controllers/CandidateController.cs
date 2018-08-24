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
    public class CandidateController : Controller
    {
        ProjectEntities db = new ProjectEntities();
        // GET: Candidate
        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewCandidateList");
        }

        [HttpGet]
        public ActionResult AddCandidate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddCandidate(Candidates candidate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Candidates.Add(candidate);
                    db.SaveChanges();
                    return RedirectToAction("ViewCandidateList");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteCandidate(int? id)
        {
            try
            {
                using (db)
                {
                    var candidate = db.Candidates.Where(x => x.ID == id).FirstOrDefault();
                    db.Candidates.Remove(candidate);
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
            return RedirectToAction("AddCandidate");
        }

        [HttpGet]
        public ActionResult EditCandidate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        [HttpPost]
        public ActionResult EditCandidate(Candidates candidate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(candidate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(candidate);
            }
            catch
            {
                return View();
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewCandidateList()
        {
            return View(db.Candidates.ToList());
        }

        public ActionResult SubmitSurvey()
        {
            return View();
        }

    }
}