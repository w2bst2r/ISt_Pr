using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StajProject.Models;

namespace StajProject.Controllers
{
    public class AnswerController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewAnswerList");
        }

        [OverrideActionFilters]
        public ActionResult ViewAnswerList()
        {
            return View(db.Answers.ToList());
        }

    }
}

