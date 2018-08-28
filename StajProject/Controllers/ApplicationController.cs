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
        //--------------------Email Part--------------------------------------------------
        //this is just to see the survey sent

        //public static async Task<string> EMailTemplate(string template)
        //{
        //    var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
        //    StreamReader objstreamreaderfile = new StreamReader(templateFilePath);
        //    var body = await objstreamreaderfile.ReadToEndAsync();
        //    objstreamreaderfile.Close();
        //    return body;
        //}


            //this runs when Send Survey is clicked on
        public async Task<ActionResult> SendEmail(SendEmailViewModel model)
        {
            //message contains the body of the email
            //var message = await EMailTemplate("WelcomeEmail");
            //replace all occurence of ViewBag.Name with the name
            //message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FirstName));
            if (db.Applications.Find(model.ID)!= null)
            {
                db.Applications.Find(model.ID).IsSent = true;
                db.SaveChanges();
            }
            var mailbody = $@"Hello {model.FirstName} {model.Surname}, <br /><br />   
            Please complete the survey in the link below: http://http://localhost:63481/Application/SubmitSurvey/?applicationID={model.ID} <br />
            If you encounter any problem, please contact the administrator  <br /><br />             
            Cheers, ";
            await MessageServices.SendEmailAsync(model.Email, "Welcome!", mailbody);
            ModelState.AddModelError("", "An Error has occured");
            return RedirectToAction("EmailSent");
        }


        public ActionResult SubmitSurvey(int applicationID)
        {
            try
            {
                var questionList = db.Questions.Select(x => x.Question).ToList();
                ViewBag.questionList = questionList;
                TempData["applicationID"] = applicationID;
                TempData.Keep("applicationID");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                throw;
            }
        }

        [HttpPost]
        public ActionResult SubmitSurvey(AnswerList answers)
        {
            try
            {
                var questionList = db.Questions.Select(x => x.Question).ToList();
                ViewBag.questionList = questionList;
                var applicationID= TempData["applicationID"];

                db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 1, CandidateAnswer = answers.Answer1, ManagerAnswer = null });
                db.Answers.Add(new Answers { ApplicationID = (int)applicationID, QuestionID = 2, CandidateAnswer = answers.Answer2, ManagerAnswer = null });
                db.Answers.Add(new Answers {ApplicationID = (int)applicationID, QuestionID = 3, CandidateAnswer = answers.Answer3, ManagerAnswer = null });
                db.SaveChanges();
                return RedirectToAction("ViewAnswerList", "Answer");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                throw;
            }
        }

        public ActionResult EmailSent()
        {
            return View();
        }
        //--------------------Email Part End--------------------------------------------------
        public ActionResult AssignManager()
        {
                ViewBag.ApplicationList = new SelectList(db.Applications, "ID", "ID");
                ViewBag.ManagerList = new SelectList(db.Managers, "ID", "FirstName");
                return View();
         }
       

        [HttpPost]
        public ActionResult AssignManager(Application_Manager application_manager)
        {
            try
            {
                ViewBag.ApplicationList = new SelectList(db.Applications, "ID", "ID");
                ViewBag.ManagerList = new SelectList(db.Managers, "ID", "FirstName");
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
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult AssignRecruiter()
        {
            ViewBag.ApplicationList = new SelectList(db.Applications, "ID", "ID");
            ViewBag.RecruiterList = new SelectList(db.Recruiters, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignRecruiter(Application_Recruiter application_recruiter)
        {
            try
            {
                ViewBag.ApplicationList = new SelectList(db.Applications, "ID", "ID");
                ViewBag.RecruiterList = new SelectList(db.Recruiters, "ID", "Name");
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
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult AddApplication()
        {
            //SelectList(datatable, Value,  WhatToShow in the list)
            ViewBag.CandidateList = new SelectList(db.Candidates, "ID", "FullName");
            ViewBag.PositionList = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeList = new SelectList(db.Grades, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddApplication(Applications application)
        {
            ViewBag.CandidateList = new SelectList(db.Candidates, "ID", "FullName");
            ViewBag.PositionList = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeList = new SelectList(db.Grades, "ID", "Name");
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
                ViewBag.RecruiterList = new SelectList(db.Grades, "ID", "FullName");
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
                ViewBag.CandidateList = new SelectList(db.Candidates, "ID", "FirstName");
                ViewBag.PositionList = new SelectList(db.Positions, "ID", "Name");
                ViewBag.GradeList = new SelectList(db.Grades, "ID", "Name");
                ViewBag.ManagerList = new SelectList(db.Managers, "ID", "FullName");
                ViewBag.RecruiterList = new SelectList(db.Grades, "ID", "FullName");
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
            //var dsaf = db.Application_Manager.Where(x => x.ApplicationID == x.Applications.ID).FirstOrDefault();
            var dsaf = db.Applications.ToList();

            return View(db.Applications.ToList());
        }

    }
}
