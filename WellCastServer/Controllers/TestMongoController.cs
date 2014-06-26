using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WellCastServer.Controllers
{
    public class TestMongoController : Controller
    {
        //
        // GET: /TestMongo/

        public ActionResult Index()
        {
            const string ConnectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();

            var databaseNames = server.GetDatabaseNames();
            var serverSettings = server.Settings;
            var database = server.GetDatabase("blog");



            return View();
        }

        public ActionResult TestRealMongo()
        {
            //Connect to MongoDB in C# with Credentials

            var credential = MongoCredential.CreateMongoCRCredential("alertsmd", "rangle", "m3anstack");
            //Server settings
            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress("ds033757.mongolab.com",33757)
            };
 
            //Get a Reference to the Client Object
            var mongoClient = new MongoClient(settings);
            var mongoServer = mongoClient.GetServer();
            var database = mongoServer.GetDatabase("alertsmd");

            var collections = database.GetCollectionNames();

            var usersCollection = database.GetCollection("users");


            var mconditions = database.GetCollection("conditions").FindAll();
            var mfeedbacks = database.GetCollection("feedbacks").FindAll();
            var mprofiles = database.GetCollection("profiles").FindAll();
            var msessions = database.GetCollection("sessions").FindAll();
            var msymptoms = database.GetCollection("symptoms").FindAll();
            var msystem_indexes = database.GetCollection("system.indexes").FindAll();
            var msystem_users = database.GetCollection("system.users").FindAll();
            var muserDatas = database.GetCollection("userDatas").FindAll();
            var musers = database.GetCollection("users").FindAll();


            return View();
        }

    }
}
