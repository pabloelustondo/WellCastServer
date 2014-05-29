using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class HomeController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.conditions = db.WellCastConditions.ToList();
            ViewBag.symptomCategories = db.WellCastSymptomCategories.ToList();
            ViewBag.symptoms = db.WellCastSymptoms.ToList();
            ViewBag.locations = db.WellCastLocations.ToList();
            ViewBag.profiles = db.WellCastProfiles.ToList();
            ViewBag.users = db.WellCastUsers.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

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
