using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WellCastServer.Models;

namespace WellCastServer
{
    public class WellCastServerEngine
    {
        private WellCastServerContext db = new WellCastServerContext();
        public MongoDatabase mdb;

        public WellCastServerEngine()
        {
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

        public string deleteForecast() {


            foreach (var cf in db.WellCastConditionForecasts) {
                            db.WellCastConditionForecasts.Remove(cf);            
            }
            foreach (var f in db.WellCastForecasts) {
                    db.WellCastForecasts.Remove(f);            
            }
            db.SaveChanges();
            return "All existing forecast or condition forecasts where removed (if any)";
        }

        public User getOneUser() {


            var musers = mdb.GetCollection("userDatas").FindAll();
            User wuser = new User();
            var muser = musers.First();
            var muserID = muser["_id"].ToString();
            wuser.ID = muser["_id"].ToString();

                    var profileIDs = muser["profiles"].AsBsonArray.ToList();
                    wuser.ProfileMIDs = new List<string>();
                    foreach (var profileID in profileIDs)
                    {
                        wuser.ProfileMIDs.Add(profileID.ToString());
                    }

            return wuser;
        }

        public List<User> getAllUsers() {

            List<User> wusers = new List<Models.User>();
            var musers = mdb.GetCollection("userDatas").FindAll();
            var profiles = mdb.GetCollection("profiles").FindAll();
            Dictionary<String, Object> profilesDic = new Dictionary<string, object>();

            foreach (var profile in profiles)
            {
                profilesDic.Add(profile["_id"].ToString(), profile);
            }



            foreach (var muser in musers)
            {
                User wuser = new User();
                var muserID = muser["_id"].ToString();
                wuser.ID = muser["_id"].ToString();

                try
                {
                    var locations = muser["locations"].AsBsonArray.ToList();
                    wuser.Locations = new List<Location>();
                    foreach (var mlocation in locations)
                    {
                        Location wlocation = new Location();
                        try { wlocation.ID = mlocation["_id"].ToString(); }
                        catch (Exception) { };
                        try { wlocation.Name = mlocation["name"].ToString(); }
                        catch (Exception) { };
                        try { wlocation.Description = mlocation["description"].ToString(); }
                        catch (Exception) { };

                        try { wlocation.lat = Convert.ToDouble(mlocation["latitude"].ToString()); }
                        catch (Exception) { };
                        try { wlocation.lon = Convert.ToDouble(mlocation["longitude"].ToString()); }
                        catch (Exception) { };

                        wuser.Locations.Add(wlocation);
                    }
                }
                catch (Exception) { };

                try
                {
                    var profileIDs = muser["profiles"].AsBsonArray.ToList();
                    wuser.ProfileMIDs = new List<string>();
                    wuser.Profiles = new List<Profile>();

                    int max = 10;
                    foreach (var profileID in profileIDs)
                    {
                        wuser.ProfileMIDs.Add(profileID.ToString());
                        dynamic mprofile;
                        profilesDic.TryGetValue(profileID.ToString(), out mprofile);
                        Profile wprofile = new Profile();
                        try { wprofile.ID = mprofile["_id"].ToString(); }
                        catch (Exception) { };
                        try { wprofile.Name = mprofile["name"].ToString(); }
                        catch (Exception) { };
                        try { wprofile.Gender = mprofile["gender"].ToString(); }
                        catch (Exception) { };
                        try { wprofile.Age = Convert.ToInt16(mprofile["age"].ToString()); }
                        catch (Exception) { };

                        try
                        {
                            var conditions = mprofile["conditions"].AsBsonArray.ToList();
                            wprofile.ConditionIDs = new List<string>();
                            foreach (var condition in conditions) { wprofile.ConditionIDs.Add(condition.ToString()); }
                        }
                        catch (Exception) { };


                        wuser.Profiles.Add(wprofile);
                        max--;
                        if (max == 0) break;
                    }
                }

                catch (Exception) { };

                wusers.Add(wuser);
            }

            return wusers;
        }

        public void logError(string label, string message)
        {
            WellCastLog wellcastlog = new WellCastLog();

            wellcastlog.Label = label;
            wellcastlog.Message = message;
            wellcastlog.timeStamp = DateTime.Now;
            db.WellCastLogs.Add(wellcastlog);
            db.SaveChanges();
        }

        public string calculateNewForecast(){
          //this process will calculate forecast for all profiles, locations and conditions
          //using corresponding the year, month, day and time slot

          //for now I will calcualte this hour
            string returnMessage = "Ok";
            DateTime now = DateTime.Now;
            DateTime forecastingDate = new DateTime(now.Year,now.Month,now.Day).AddHours(now.Hour);

            DateTime lastforecast = new DateTime();

            try { lastforecast = db.WellCastConditionForecasts.Select(f => f.Date).Max(); }
            catch (Exception) { };

            if (lastforecast == forecastingDate) { return "Forecast already calculated for date-hour: " + forecastingDate; };
    //get all profiles

            var users = getAllUsers();
            var conditions = db.WellCastConditions.ToList();
            List<Condition> conditionSubset;

            int loops = 0;
            Random rnd1 = new Random();
            foreach (var user in users) 
            {
                loops++;
            foreach (var profile in user.Profiles)
            {

                foreach (var location in user.Locations)
                {

                        Forecast forecast = new Forecast();

                        forecast.ProfileMID = profile.ID;
                        forecast.LocationMID = location.ID;
                        forecast.UserMID = user.ID;
                        forecast.Date = forecastingDate;
                        forecast.ID = Guid.NewGuid();

                        //now the ramdom thing
                        Random random = new Random();
                        forecast.RiskDay0 = random.Next(0, 6);
                        forecast.RiskDay1 = random.Next(0, 6);
                        forecast.RiskDay2 = random.Next(0, 6);
                        forecast.RiskDay3 = random.Next(0, 6);
                        forecast.RiskDay4 = random.Next(0, 6);
                        forecast.RiskDay5 = random.Next(0, 6);

                        forecast.ReportDay0 = "";
                        forecast.ReportDay1 = "";
                        forecast.ReportDay2 = "";
                        forecast.ReportDay3 = "";
                        forecast.ReportDay4 = "";
                        forecast.ReportDay5 = "";

                        db.WellCastForecasts.Add(forecast);
                        db.SaveChanges();

                        foreach (var conditionKeyName in profile.ConditionIDs)
                        {
                            Condition condition = new Condition();
                            try
                            {
                                condition = db.WellCastConditions.Where(c => c.KeyName == conditionKeyName).First();
                            }
                            catch (Exception e)
                            {
                                if (e.Message == "Sequence contains no elements") {
                                    try { 
                                    condition = new Condition();
                                    condition.ID = conditionKeyName;
                                    condition.KeyName = conditionKeyName;
                                    condition.Name = conditionKeyName;
                                    condition.Description = conditionKeyName;
                                    condition.Validated = false;
                                    db.WellCastConditions.Add(condition);
                                    db.SaveChanges();
                                    }
                                    catch (Exception e2)
                                    {
                                        logError("failed to create condition from mongo", e2.Message);
                                    }
                                }                                
                            }

                            if (conditionKeyName != null) 
                            {
                            ConditionForecast conditionforecast = new ConditionForecast();
                            conditionforecast.ForecastID = forecast.ID;
                            conditionforecast.ProfileMID = profile.ID;
                            conditionforecast.LocationMID = location.ID;
                            conditionforecast.ConditionID = condition.ID;
                            conditionforecast.Date = forecastingDate;
                            conditionforecast.ID = Guid.NewGuid();

                            //now the ramdom thing
                            conditionforecast.RiskDay0 = random.Next(0, 6);
                            if (conditionforecast.RiskDay0 > 2) forecast.ReportDay0 += "risk of " + conditionKeyName;
                            conditionforecast.RiskDay1 = random.Next(0, 6);
                            if (conditionforecast.RiskDay1 > 2) forecast.ReportDay1 += "risk of " + conditionKeyName;
                            conditionforecast.RiskDay2 = random.Next(0, 6);
                            if (conditionforecast.RiskDay2 > 2) forecast.ReportDay2 += "risk of " + conditionKeyName;                
                            conditionforecast.RiskDay3 = random.Next(0, 6);
                            if (conditionforecast.RiskDay3 > 2) forecast.ReportDay3 += "risk of " + conditionKeyName;      
                            conditionforecast.RiskDay4 = random.Next(0, 6);
                            if (conditionforecast.RiskDay4 > 2) forecast.ReportDay4 += "risk of " + conditionKeyName;
                            conditionforecast.RiskDay5 = random.Next(0, 6);
                            if (conditionforecast.RiskDay5 > 2) forecast.ReportDay5 += "risk of " + conditionKeyName;


                            db.WellCastConditionForecasts.Add(conditionforecast);
                        }
                        }

                        if (forecast.ReportDay0 == "") forecast.ReportDay0 = "no reported risk today";
                        if (forecast.ReportDay1 == "") forecast.ReportDay1 = "no reported risk today";
                        if (forecast.ReportDay2 == "") forecast.ReportDay2 = "no reported risk today";
                        if (forecast.ReportDay3 == "") forecast.ReportDay3 = "no reported risk today";
                        if (forecast.ReportDay4 == "") forecast.ReportDay4 = "no reported risk today";
                        if (forecast.ReportDay5 == "") forecast.ReportDay5 = "no reported risk today";

                        db.Entry(forecast).State = EntityState.Modified;
                        db.SaveChanges();
                }//end for each location
            }//end for each profile
            }//end for each usser
            db.SaveChanges();
            returnMessage = "Forecast where calculated for date-hour: " + forecastingDate;
            return returnMessage;
        }

    }
}