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
    public class LogController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View(db.WellCastLogs.ToList());
        }

        //
        // GET: /Log/Details/5

        public ActionResult Details(int id = 0)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            if (wellcastlog == null)
            {
                return HttpNotFound();
            }
            return View(wellcastlog);
        }

        //
        // GET: /Log/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Log/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WellCastLog wellcastlog)
        {
            if (ModelState.IsValid)
            {
                db.WellCastLogs.Add(wellcastlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wellcastlog);
        }

        //
        // GET: /Log/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            if (wellcastlog == null)
            {
                return HttpNotFound();
            }
            return View(wellcastlog);
        }

        //
        // POST: /Log/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WellCastLog wellcastlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wellcastlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wellcastlog);
        }

        //
        // GET: /Log/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            if (wellcastlog == null)
            {
                return HttpNotFound();
            }
            return View(wellcastlog);
        }

        //
        // POST: /Log/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            db.WellCastLogs.Remove(wellcastlog);
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