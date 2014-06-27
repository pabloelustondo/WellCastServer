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
            var wellcastconditionforecasts = db.WellCastConditionForecasts.Include(c => c.Forecast).Include(c => c.Condition);
            return View(wellcastconditionforecasts.ToList());
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