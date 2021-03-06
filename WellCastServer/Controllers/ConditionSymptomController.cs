﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class ConditionSymptomController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /ConditionSymptom/

        public ActionResult Index()
        {
            var wellcastconditionsymptoms = db.WellCastConditionSymptoms.Include(c => c.Condition).Include(c => c.Symptom);
            return View(wellcastconditionsymptoms.ToList());
        }

        //
        // GET: /ConditionSymptom/Details/5

        public ActionResult Details(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            if (conditionsymptom == null)
            {
                return HttpNotFound();
            }
            return View(conditionsymptom);
        }

        //
        // GET: /ConditionSymptom/Create

        public ActionResult Create()
        {
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "KeyName");
            ViewBag.SymptomID = new SelectList(db.WellCastSymptoms, "ID", "KeyName");
            return View();
        }

        //
        // POST: /ConditionSymptom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConditionSymptom conditionsymptom)
        {
            if (ModelState.IsValid)
            {
                conditionsymptom.ID = Guid.NewGuid();
                db.WellCastConditionSymptoms.Add(conditionsymptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "KeyName", conditionsymptom.ConditionID);
            ViewBag.SymptomID = new SelectList(db.WellCastSymptoms, "ID", "SymptomCategoryID", conditionsymptom.SymptomID);
            return View(conditionsymptom);
        }

        //
        // GET: /ConditionSymptom/Edit/5

        public ActionResult Edit(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            if (conditionsymptom == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "KeyName", conditionsymptom.ConditionID);
            ViewBag.SymptomID = new SelectList(db.WellCastSymptoms, "ID", "SymptomCategoryID", conditionsymptom.SymptomID);
            return View(conditionsymptom);
        }

        //
        // POST: /ConditionSymptom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConditionSymptom conditionsymptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conditionsymptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConditionID = new SelectList(db.WellCastConditions, "ID", "KeyName", conditionsymptom.ConditionID);
            ViewBag.SymptomID = new SelectList(db.WellCastSymptoms, "ID", "SymptomCategoryID", conditionsymptom.SymptomID);
            return View(conditionsymptom);
        }

        //
        // GET: /ConditionSymptom/Delete/5

        public ActionResult Delete(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            if (conditionsymptom == null)
            {
                return HttpNotFound();
            }
            return View(conditionsymptom);
        }

        //
        // POST: /ConditionSymptom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            db.WellCastConditionSymptoms.Remove(conditionsymptom);
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