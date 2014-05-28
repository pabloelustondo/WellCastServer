using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class ConditionProfileController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /ConditionProfile/

        public ActionResult Index()
        {
            var conditionprofiles = db.WellCastConditionProfiles.Include(c => c.Condition).Include(c => c.Profile);
            return View(conditionprofiles.ToList());
        }

        //
        // GET: /ConditionProfile/Details/5

        public ActionResult Details(Guid id)
        {
            ConditionProfile conditionprofile = db.WellCastConditionProfiles.Find(id);
            if (conditionprofile == null)
            {
                return HttpNotFound();
            }
            return View(conditionprofile);
        }

        //
        // GET: /ConditionProfile/Create

        public ActionResult Create()
        {
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name");
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name");
            return View();
        }

        //
        // POST: /ConditionProfile/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConditionProfile conditionprofile)
        {
            if (ModelState.IsValid)
            {
                conditionprofile.ID = Guid.NewGuid();
                db.WellCastConditionProfiles.Add(conditionprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionprofile.ConditionID);
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionprofile.ProfileID);
            return View(conditionprofile);
        }

        //
        // GET: /ConditionProfile/Edit/5

        public ActionResult Edit(Guid id)
        {
            ConditionProfile conditionprofile = db.WellCastConditionProfiles.Find(id);
            if (conditionprofile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionprofile.ConditionID);
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionprofile.ProfileID);
            return View(conditionprofile);
        }

        //
        // POST: /ConditionProfile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConditionProfile conditionprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conditionprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionprofile.ConditionID);
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionprofile.ProfileID);
            return View(conditionprofile);
        }

        //
        // GET: /ConditionProfile/Delete/5

        public ActionResult Delete(Guid id)
        {
            ConditionProfile conditionprofile = db.WellCastConditionProfiles.Find(id);
            if (conditionprofile == null)
            {
                return HttpNotFound();
            }
            return View(conditionprofile);
        }

        //
        // POST: /ConditionProfile/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ConditionProfile conditionprofile = db.WellCastConditionProfiles.Find(id);
            db.WellCastConditionProfiles.Remove(conditionprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}