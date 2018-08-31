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
    public class PositionController : Controller
    {
        ProjectEntities db = new ProjectEntities();

        [OverrideActionFilters]
        public ActionResult Index()
        {
            return RedirectToAction("ViewPositionList");
        }
        public ActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPosition(Positions position)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Positions.Add(position);
                    db.SaveChanges();
                    return RedirectToAction("ViewPositionList");
                }
                return View(position);
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult DeletePosition(int? id)
        {
            try
            {
                using (db)
                {
                    var position = db.Positions.Where(x => x.ID == id).FirstOrDefault();
                    db.Positions.Remove(position);
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
            return RedirectToAction("AddPosition");
        }

        [HttpGet]
        public ActionResult EditPosition(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var position = db.Positions.Find(id);
                if (position == null)
                {
                    return HttpNotFound();
                }
                return View(position);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditPosition(Positions position)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(position).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(position);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [OverrideActionFilters]
        public ActionResult ViewPositionList()
        {
            return View(db.Positions.ToList());
        }

    }
}