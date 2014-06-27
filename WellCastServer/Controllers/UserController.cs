using MongoDB.Driver;
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
    public class UserController : WellCastController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            var wusers = wellCastServerEngine.getAllUsers();
            return View(wusers);
        }

        //
        // GET: /User/Details/5


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}