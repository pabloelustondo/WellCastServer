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
    public class ConditionForecastController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /ConditionForecast/

        public ActionResult Index()
        {
            var conditionforecasts = db.WellCastConditionForecasts.Include(c => c.Profile).Include(c => c.Location).Include(c => c.Condition);
            return View(conditionforecasts.ToList());
        }

        //
        // GET: /ConditionForecast/Details/5

        public ActionResult Details(Guid id)
        {
            ConditionForecast conditionforecast = db.WellCastConditionForecasts.Find(id);
            if (conditionforecast == null)
            {
                return HttpNotFound();
            }
            return View(conditionforecast);
        }

        //
        // GET: /ConditionForecast/Create

        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name");
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name");
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name");
            return View();
        }

        //
        // POST: /ConditionForecast/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConditionForecast conditionforecast)
        {
            if (ModelState.IsValid)
            {
                conditionforecast.ID = Guid.NewGuid();
                db.WellCastConditionForecasts.Add(conditionforecast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionforecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", conditionforecast.LocationID);
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionforecast.ConditionID);
            return View(conditionforecast);
        }

        //
        // GET: /ConditionForecast/Edit/5

        public ActionResult Edit(Guid id)
        {
            ConditionForecast conditionforecast = db.WellCastConditionForecasts.Find(id);
            if (conditionforecast == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionforecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", conditionforecast.LocationID);
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionforecast.ConditionID);
            return View(conditionforecast);
        }

        //
        // POST: /ConditionForecast/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConditionForecast conditionforecast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conditionforecast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", conditionforecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", conditionforecast.LocationID);
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "Name", conditionforecast.ConditionID);
            return View(conditionforecast);
        }

        //
        // GET: /ConditionForecast/Delete/5

        public ActionResult Delete(Guid id)
        {
            ConditionForecast conditionforecast = db.WellCastConditionForecasts.Find(id);
            if (conditionforecast == null)
            {
                return HttpNotFound();
            }
            return View(conditionforecast);
        }

        //
        // POST: /ConditionForecast/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ConditionForecast conditionforecast = db.WellCastConditionForecasts.Find(id);
            db.WellCastConditionForecasts.Remove(conditionforecast);
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