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
    public class ConditionController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Condition/

        public ActionResult Index()
        {
            return View(db.WellCastConditions.ToList());
        }

        //
        // GET: /Condition/Details/5

        public ActionResult Details(int id = 0)
        {
            Condition condition = db.WellCastConditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        //
        // GET: /Condition/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Condition/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.WellCastConditions.Add(condition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(condition);
        }

        //
        // GET: /Condition/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Condition condition = db.WellCastConditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        //
        // POST: /Condition/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(condition);
        }

        //
        // GET: /Condition/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Condition condition = db.WellCastConditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            return View(condition);
        }

        //
        // POST: /Condition/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condition condition = db.WellCastConditions.Find(id);
            db.WellCastConditions.Remove(condition);
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