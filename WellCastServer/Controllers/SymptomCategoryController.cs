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
    public class SymptomCategoryController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /SymptomCategory/

        public ActionResult Index()
        {
            return View(db.WellCastSymptomCategories.ToList());
        }

        //
        // GET: /SymptomCategory/Details/5

        public ActionResult Details(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            if (symptomcategory == null)
            {
                return HttpNotFound();
            }
            return View(symptomcategory);
        }

        //
        // GET: /SymptomCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SymptomCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SymptomCategory symptomcategory)
        {
            if (ModelState.IsValid)
            {
                symptomcategory.ID = Guid.NewGuid();
                db.WellCastSymptomCategories.Add(symptomcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(symptomcategory);
        }

        //
        // GET: /SymptomCategory/Edit/5

        public ActionResult Edit(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            if (symptomcategory == null)
            {
                return HttpNotFound();
            }
            return View(symptomcategory);
        }

        //
        // POST: /SymptomCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SymptomCategory symptomcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptomcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(symptomcategory);
        }

        //
        // GET: /SymptomCategory/Delete/5

        public ActionResult Delete(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            if (symptomcategory == null)
            {
                return HttpNotFound();
            }
            return View(symptomcategory);
        }

        //
        // POST: /SymptomCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            db.WellCastSymptomCategories.Remove(symptomcategory);
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