using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellCastServer;
using WellCastServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WellCastServerTest
{
    [TestClass]
    public class WellCastServerEngineTest
    {
        [TestMethod]
        public void addAlert2UserTest()
        {
            WellCastServerEngine mm = new WellCastServerEngine();

            User user = new User();

            user.ID = "542c53a302d6a4910db3fdd7";
            BsonArray forecastIDs = new BsonArray();


            string id1 = Guid.NewGuid().ToString();
            string id2 = Guid.NewGuid().ToString();

            forecastIDs.Add(id1);
            forecastIDs.Add(id2);

            MongoDatabase mdb;
            //Get a Reference to the Client Object
            var mongoClient = new MongoClient("mongodb://pelustondo:san10ro4@ds041140.mongolab.com:41140/wellcast");
            var mongoServer = mongoClient.GetServer();
            mdb = mongoServer.GetDatabase("wellcast");     

            var queryDoc2 = new QueryDocument { { "state", "unsent" } };
            // Query the db for a document with the required ID 
            var alertsUnSent0 = mdb.GetCollection("alerts").Find(queryDoc2);
            long alerts0 = alertsUnSent0.Count();
                 
            mm.addAlert2User(user, forecastIDs);

            var alertsUnSent1 = mdb.GetCollection("alerts").Find(queryDoc2);
            long alerts1 = alertsUnSent1.Count();

            
            Assert.AreEqual(alerts1, alerts0 + 1);


        }
    }
}
