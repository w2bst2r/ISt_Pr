using StajProject.Filters;
using StajProject.Models;
using StajProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace StajProject.Controllers
{
    public class ApplicationController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewApplicationList");
        }

        public ActionResult Jquery()
        {
            return View();
        }

        public async Task<ActionResult> SendEmail(SendEmailViewModel model)
        {
            try
            {
                if (db.Applications.Find(model.ApplicationID) != null)
                {
                    db.Applications.Find(model.ApplicationID).IsSent = true;
                    db.SaveChanges();
                }
            var candidateMail = $@"Hello {model.CandidateFullName}, <br /><br />   
            Please complete the survey in the link below: http://localhost:63481/Application/SubmitSurvey/?applicationID={model.ApplicationID}&isCandidate=true <br />
            If you encounter any problem, please contact the administrator  <br /><br />             
            Cheers, ";
            var managerMail = $@"Hello {model.ManagerFullName}, <br /><br />   
            Please complete the survey in the link below: http://http://localhost:63481/Application/SubmitSurvey/?applicationID={model.ApplicationID}&isCandidate=false <br />
            If you encounter any problem, please contact the administrator  <br /><br />             
            Cheers, ";
                await MessageServices.SendEmailAsync(model.CandidateEmail, "Welcome!", candidateMail);
                await MessageServices.SendEmailAsync(model.ManagerEmail, "Welcome!", managerMail);
                return RedirectToAction("EmailSent");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SubmitSurvey(int applicationID, bool isCandidate)
        {
            try
            {
                var questionList = db.Questions.Select(x => x.Question).ToList();
                ViewBag.questionList = questionList;
                TempData["applicationID"] = applicationID;
                TempData.Keep("applicationID");
                TempData["isCandidate"] = isCandidate;
                TempData.Keep("isCandidate");
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SubmitSurvey(AnswerList answers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.questionList = db.Questions.Select(x => x.Question).ToList();
                    var applicationID = TempData["applicationID"];
                    bool isCandidate = (bool)TempData["isCandidate"];
                    var rowToUpdate1 = db.Answers.Find(1, applicationID);
                    var rowToUpdate2 = db.Answers.Find(2, applicationID);
                    var rowToUpdate3 = db.Answers.Find(3, applicationID);
                    if (isCandidate)
                    {
                        if (rowToUpdate1 != null && rowToUpdate2 != null && rowToUpdate3 != null)
                        {
                            rowToUpdate1.CandidateAnswer = answers.Answer1;
                            rowToUpdate2.CandidateAnswer = answers.Answer1;
                            rowToUpdate3.CandidateAnswer = answers.Answer1;
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 1, CandidateAnswer = answers.Answer1 });
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 2, CandidateAnswer = answers.Answer2 });
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 3, CandidateAnswer = answers.Answer3 });
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (rowToUpdate1 != null && rowToUpdate2 != null && rowToUpdate3 != null)
                        {
                            rowToUpdate1.ManagerAnswer = answers.Answer1;
                            rowToUpdate2.ManagerAnswer = answers.Answer1;
                            rowToUpdate3.ManagerAnswer = answers.Answer1;
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 1, ManagerAnswer = answers.Answer1 });
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 2, ManagerAnswer = answers.Answer2 });
                            db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 3, ManagerAnswer = answers.Answer3 });
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("ViewAnswerList", "Answer");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    throw;
                }
            }
            ModelState.AddModelError("", "Error");
            return View();
        }

        public ActionResult EmailSent()
        {
            return View();
        }

        public ActionResult AddApplication()
        {
            //SelectList(datatable, Value,  WhatToShow in the list)
            ViewBag.CandidateList = new SelectList(db.Candidates, "ID", "FullName");
            ViewBag.ManagerList = new SelectList(db.Managers, "ID", "FullName");
            ViewBag.RecruiterList = new SelectList(db.Recruiters, "ID", "FullName");
            ViewBag.PositionList = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeList = new SelectList(db.Grades, "ID", "Name");
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
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException;
                return View();
            }
        }

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
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ClearForm()
        {
            ModelState.Clear();
            return RedirectToAction("AddApplication");
        }

        public ActionResult EditApplication(int? id)
        {
            try
            {
                ViewBag.CandidateList = new SelectList(db.Candidates, "ID", "FirstName");
                ViewBag.PositionList = new SelectList(db.Positions, "ID", "Name");
                ViewBag.GradeList = new SelectList(db.Grades, "ID", "Name");
                ViewBag.ManagerList = new SelectList(db.Managers, "ID", "FullName");
                ViewBag.RecruiterList = new SelectList(db.Recruiters, "ID", "FullName");
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var application = db.Applications.Find(id);
                if (application == null)
                {
                    return HttpNotFound();
                }
                return View(application);
            }
            catch (Exception)
            {
                throw;
            }
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
                else return Content("Model state is not Valid");
            }
            catch(Exception)
            {
                throw;
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewApplicationList()
        {
            return View(db.Applications.ToList());
        }

    }
}
