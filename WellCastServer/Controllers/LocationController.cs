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
    public class LocationController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Location/

        public ActionResult Index()
        {
            var wellcastlocations = db.WellCastLocations.Include(l => l.User);
            return View(wellcastlocations.ToList());
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(Guid id)
        {
            Location location = db.WellCastLocations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        //
        // POST: /Location/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                location.ID = Guid.NewGuid();
                db.WellCastLocations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", location.UserID);
            return View(location);
        }

        //
        // GET: /Location/Edit/5

        public ActionResult Edit(Guid id)
        {
            Location location = db.WellCastLocations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", location.UserID);
            return View(location);
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", location.UserID);
            return View(location);
        }

        //
        // GET: /Location/Delete/5

        public ActionResult Delete(Guid id)
        {
            Location location = db.WellCastLocations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Location location = db.WellCastLocations.Find(id);
            db.WellCastLocations.Remove(location);
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