using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WellCastServer.Controllers
{
    public class WellCastController : Controller
    {
        public MongoDatabase mdb;
        //
        // GET: /WellCast/
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {


            var credential = MongoCredential.CreateMongoCRCredential("alertsmd", "rangle", "m3anstack");
            //Server settings
            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress("ds033757.mongolab.com", 33757)
            };

            //Get a Reference to the Client Object
            var mongoClient = new MongoClient(settings);
            var mongoServer = mongoClient.GetServer();
            mdb = mongoServer.GetDatabase("alertsmd");        
        }

    }
}
