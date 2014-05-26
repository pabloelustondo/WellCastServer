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
    public class ErrorMessageController : Controller
    {
        private WellCastServerContext db = new WellCastServerContext();

        public ActionResult Index()
        {
            return View();
        }
    }
   
}