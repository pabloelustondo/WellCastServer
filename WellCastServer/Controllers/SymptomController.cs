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
    public class SymptomController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Symptom/

        public ActionResult Index()
        {
            var wellcastsymptoms = db.WellCastSymptoms.Include(s => s.SymptomCategory);
            return View(wellcastsymptoms.ToList());
        }

        //
        // GET: /Symptom/Details/5

        public ActionResult Details(Guid id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        //
        // GET: /Symptom/Create

        public ActionResult Create()
        {
            ViewBag.SymptomCategoryID = new SelectList(db.WellCastSymptomCategories, "ID", "Name");
            return View();
        }

        //
        // POST: /Symptom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                symptom.ID = symptom.Name;
                db.WellCastSymptoms.Add(symptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SymptomCategoryID = new SelectList(db.WellCastSymptomCategories, "ID", "Name", symptom.SymptomCategoryID);
            return View(symptom);
        }

        //
        // GET: /Symptom/Edit/5

        public ActionResult Edit(Guid id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            ViewBag.SymptomCategoryID = new SelectList(db.WellCastSymptomCategories, "ID", "Name", symptom.SymptomCategoryID);
            return View(symptom);
        }

        //
        // POST: /Symptom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SymptomCategoryID = new SelectList(db.WellCastSymptomCategories, "ID", "Name", symptom.SymptomCategoryID);
            return View(symptom);
        }

        //
        // GET: /Symptom/Delete/5

        public ActionResult Delete(Guid id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }
            return View(symptom);
        }

        //
        // POST: /Symptom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            db.WellCastSymptoms.Remove(symptom);
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