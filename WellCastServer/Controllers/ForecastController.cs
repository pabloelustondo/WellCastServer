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
    public class ForecastController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            var forecasts = db.WellCastForecasts;
            return View(forecasts.ToList());
        }

        //
        // GET: /Forecast/Details/5

        public ActionResult Details(Guid id)
        {
            Forecast forecast = db.WellCastForecasts.Find(id);
            if (forecast == null)
            {
                return HttpNotFound();
            }
            return View(forecast);
        }

        //
        // GET: /Forecast/Create

        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name");
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name");
            return View();
        }

        //
        // POST: /Forecast/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Forecast forecast)
        {
            if (ModelState.IsValid)
            {
                forecast.ID = Guid.NewGuid();
                db.WellCastForecasts.Add(forecast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", forecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", forecast.LocationID);
            return View(forecast);
        }

        //
        // GET: /Forecast/Edit/5

        public ActionResult Edit(Guid id)
        {
            Forecast forecast = db.WellCastForecasts.Find(id);
            if (forecast == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", forecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", forecast.LocationID);
            return View(forecast);
        }

        //
        // POST: /Forecast/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Forecast forecast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forecast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.WellCastProfiles, "ID", "Name", forecast.ProfileID);
            ViewBag.LocationID = new SelectList(db.WellCastLocations, "ID", "Name", forecast.LocationID);
            return View(forecast);
        }

        //
        // GET: /Forecast/Delete/5

        public ActionResult Delete(Guid id)
        {
            Forecast forecast = db.WellCastForecasts.Find(id);
            if (forecast == null)
            {
                return HttpNotFound();
            }
            return View(forecast);
        }

        //
        // POST: /Forecast/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Forecast forecast = db.WellCastForecasts.Find(id);
            db.WellCastForecasts.Remove(forecast);
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