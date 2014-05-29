using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WellCastServer.Models;

namespace WellCastServer
{
    public class WellCastServerEngine
    {
        private WellCastServerContext db = new WellCastServerContext();

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
            var profiles = db.WellCastProfiles.ToList();
            var locations = db.WellCastLocations.ToList();
            var conditions = db.WellCastConditions.ToList();

            Random rnd1 = new Random();
            foreach (var profile in profiles){
                foreach (var location in locations) {
                    foreach (var condition in conditions) {
                        ConditionForecast conditionforecast = new ConditionForecast();

                        conditionforecast.ProfileID = profile.ID;
                        conditionforecast.LocationID = location.ID;
                        conditionforecast.ConditionID = condition.ID;
                        conditionforecast.Date = forecastingDate;
                        conditionforecast.ID = Guid.NewGuid();

                        //now the ramdom thing
                        Random random = new Random();
                        conditionforecast.RiskDay0 = random.Next(0, 6);
                        conditionforecast.RiskDay1 = random.Next(0, 6);
                        conditionforecast.RiskDay2 = random.Next(0, 6);
                        conditionforecast.RiskDay3 = random.Next(0, 6);
                        conditionforecast.RiskDay4 = random.Next(0, 6);
                        conditionforecast.RiskDay5 = random.Next(0, 6);

                        db.WellCastConditionForecasts.Add(conditionforecast);
                        db.SaveChanges();

                    }               
                }
            }


            foreach (var profile in profiles)
            {
                foreach (var location in locations)
                {

                        Forecast forecast = new Forecast();

                        forecast.ProfileID = profile.ID;
                        forecast.LocationID = location.ID;
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

                        db.WellCastForecasts.Add(forecast);
                        db.SaveChanges();

                }
            }
            returnMessage = "Forecast where calculated for date-hour: " + forecastingDate;
            return returnMessage;
        }

    }
}