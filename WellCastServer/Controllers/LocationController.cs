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
    public class LocationController : WellCastController
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Location/

        public ActionResult Index()
        {
            List<Location> wlocations = new List<Models.Location>();
            var mlocations = mdb.GetCollection("locations").FindAll();

            var collections = mdb.GetCollectionNames();
            foreach (var mlocation in mlocations)
            {
                Location wlocation = new Location();
                try { wlocation.ID = mlocation["_id"].ToString(); }
                catch (Exception) { };
                try { wlocation.name = mlocation["name"].ToString(); }
                catch (Exception) { };

                wlocations.Add(wlocation);
            }
            return View(wlocations);
        }

   
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}