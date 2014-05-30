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

                        forecast.ReportDay0 = "";
                        forecast.ReportDay1 = "";
                        forecast.ReportDay2 = "";
                        forecast.ReportDay3 = "";
                        forecast.ReportDay4 = "";
                        forecast.ReportDay5 = "";

                        foreach (var condition in conditions)
                        {
                            ConditionForecast conditionforecast = new ConditionForecast();
                            conditionforecast.ForecastID = forecast.ID;
                            conditionforecast.ProfileID = profile.ID;
                            conditionforecast.LocationID = location.ID;
                            conditionforecast.ConditionID = condition.ID;
                            conditionforecast.Date = forecastingDate;
                            conditionforecast.ID = Guid.NewGuid();

                            //now the ramdom thing
                            conditionforecast.RiskDay0 = random.Next(0, 6);
                            if (conditionforecast.RiskDay0 > 2) forecast.ReportDay0 += "risk of " + condition.Name;
                            conditionforecast.RiskDay1 = random.Next(0, 6);
                            if (conditionforecast.RiskDay1 > 2) forecast.ReportDay1 += "risk of " + condition.Name;
                            conditionforecast.RiskDay2 = random.Next(0, 6);
                            if (conditionforecast.RiskDay2 > 2) forecast.ReportDay2 += "risk of " + condition.Name;                
                            conditionforecast.RiskDay3 = random.Next(0, 6);
                            if (conditionforecast.RiskDay3 > 2) forecast.ReportDay3 += "risk of " + condition.Name;      
                            conditionforecast.RiskDay4 = random.Next(0, 6);
                            if (conditionforecast.RiskDay4 > 2) forecast.ReportDay4 += "risk of " + condition.Name;
                            conditionforecast.RiskDay5 = random.Next(0, 6);
                            if (conditionforecast.RiskDay5 > 2) forecast.ReportDay5 += "risk of " + condition.Name;


                            db.WellCastConditionForecasts.Add(conditionforecast);

                        }

                        if (forecast.ReportDay0 == "") forecast.ReportDay0 = "no reported risk today";
                        if (forecast.ReportDay1 == "") forecast.ReportDay1 = "no reported risk today";
                        if (forecast.ReportDay2 == "") forecast.ReportDay2 = "no reported risk today";
                        if (forecast.ReportDay3 == "") forecast.ReportDay3 = "no reported risk today";
                        if (forecast.ReportDay4 == "") forecast.ReportDay4 = "no reported risk today";
                        if (forecast.ReportDay5 == "") forecast.ReportDay5 = "no reported risk today";

                        db.WellCastForecasts.Add(forecast);
                        db.SaveChanges();
                }
            }
            returnMessage = "Forecast where calculated for date-hour: " + forecastingDate;
            return returnMessage;
        }

    }
}