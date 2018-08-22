using StajProject.Models;
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

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailViewModel model)
        {
            //message contains the body of the email
            //var message = await EMailTemplate("WelcomeEmail");
            //replace all occurence of ViewBag.Name with the name
            //message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FirstName));
            var mailbody = $@"Hello website owner,
            This is a new contact request from your website:
            Name: {model.FirstName}
            LastName: {model.Email}                    
            Cheers,
            The websites contact form";
            await MessageServices.SendEmailAsync(model.Email, "Welcome!", mailbody);
            ModelState.AddModelError("", "Email successfully sent.");
            return View("EmailSent");
        }

        public ActionResult EmailSent()
        {
            return View();
        }
   //--------------------Email Part End--------------------------------------------------
        public ActionResult AssignManager()
        {
            if (Session["Email"] != null)
            {
                //show it as Managers.ID  store it in Application.ID
                ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ID");
                //show it as Managers.Name  store it in Managers.ID
                ViewBag.ManagerID = new SelectList(db.Managers, "ID", "Name");
                return View();
            }
            else return Content("You can't access this page");
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
                ViewBag.Error = ex.Message;
                return View();
            }
        }

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
                ViewBag.Error = ex.Message;
                return View();
            }
        }

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
            ViewBag.CandidateID = new SelectList(db.Candidates, "ID", "Name");
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "Name");
            ViewBag.GradeID = new SelectList(db.Grades, "ID", "Name");
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
                ViewBag.Error = ex.Message;
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
