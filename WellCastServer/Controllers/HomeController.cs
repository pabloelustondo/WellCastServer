using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class HomeController : WellCastController
    {
   
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.conditions = db.WellCastConditions.ToList();
            ViewBag.symptomCategories = db.WellCastSymptomCategories.ToList();
            ViewBag.symptoms = db.WellCastSymptoms.ToList();


            ViewBag.forecasts = db.WellCastForecasts.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult deleteForecast()
        {

            ViewBag.message = wellCastServerEngine.deleteForecast();

            return View();
        }

        public ActionResult calculateNewForecast()
        {
            WellCastServerEngine wellCastServerEngine = new WellCastServerEngine();
            ViewBag.message = wellCastServerEngine.calculateNewForecast();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
