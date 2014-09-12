using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class ForecastsController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();
        public WellCastServerEngine mm = new WellCastServerEngine();

        // GET api/Forecasts
        public IEnumerable<Forecast> GetForecasts()
        {
            var conditions = db.WellCastForecasts.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<Forecast>>(conditions);
            return conditions;
        }

        // GET api/Forecasts/5
        public Forecast GetForecast(String id)
        {

                Guid gid = new Guid(id);
                Forecast condition = db.WellCastForecasts.Find(gid);

                return condition;
        }

        // GET api/Forecasts/5
        public List<Forecast> GetForecastsForUser(String user_id)
        {
                //here is the idea. If we have forecast of a reasonable date and one for each location and profile...then we return this
                //if not we calculate profiles for the user
                User user = mm.getUserById(user_id);
                String UserIdGuid = user_id;
                DateTime LastDate = new DateTime();
                try {
                    LastDate = db.WellCastForecasts.Where(f => f.UserMID == UserIdGuid).Max(f => f.Date);
                     }
                catch(Exception){}

                var totalminutes = (DateTime.Now - LastDate).TotalMinutes;
                if ((DateTime.Now - LastDate).TotalMinutes < mm.MaxAgeMinutes)
                {

                    int numberOfProfiles = (user.ProfileMIDs!=null)?user.ProfileMIDs.Count():0;
                    int numberOfLocations = (user.LocationMIDs != null) ? user.LocationMIDs.Count() : 0;


                    List<Forecast> conditions = db.WellCastForecasts.Where(f => f.UserMID == UserIdGuid && f.Date == LastDate).ToList();

                    if (conditions.Count == numberOfLocations * numberOfProfiles)
                    {
                        return conditions;
                    }
                }

                //if we are here, something did not go well.. so we recalculate forecast for user and start again.

                mm.calculateNewForecastForUser(user);
                LastDate = db.WellCastForecasts.Where(f => f.UserMID == UserIdGuid).Max(f => f.Date);
                List<Forecast> conditions2 = db.WellCastForecasts.Where(f => f.UserMID == UserIdGuid && f.Date == LastDate).ToList();
                return conditions2;             
        }

        // GET api/Forecasts/5
        public List<Forecast> GetForecastsForProfile(String profile_id, String user_id)
        {///////////////////////////

            Expression<Func<Forecast, bool>> hasProfileId = (f => f.ProfileMID == profile_id);
     

            var user = mm.getUserById(user_id);
            DateTime LastDate = new DateTime();
            try
            {
                LastDate = db.WellCastForecasts.Where(hasProfileId).Max(f => f.Date);
            }
            catch (Exception) { }

            Expression<Func<Forecast, bool>> hasProfileIdAndDate = (f => f.ProfileMID == profile_id && (f.Date == LastDate));

            var totalminutes = (DateTime.Now - LastDate).TotalMinutes;
            if ((DateTime.Now - LastDate).TotalMinutes < mm.MaxAgeMinutes)
            {
                int numberOfLocations = (user.LocationMIDs != null) ? user.LocationMIDs.Count() : 0;


                List<Forecast> conditions = db.WellCastForecasts.Where(hasProfileIdAndDate).ToList();

                if (conditions.Count == numberOfLocations)
                {
                    return conditions;
                }
            }

            //if we are here, something did not go well.. so we recalculate forecast for user and start again.

            mm.calculateNewForecastForUser(user);
            LastDate = db.WellCastForecasts.Where(hasProfileId).Max(f => f.Date);
            List<Forecast> conditions2 = db.WellCastForecasts.Where(hasProfileIdAndDate).ToList();
            return conditions2;     

        }

        // PUT api/Forecasts/5
        public HttpResponseMessage PutForecast(Guid id, Forecast forecast)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != forecast.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(forecast).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Forecasts
        public HttpResponseMessage PostForecast(Forecast forecast)
        {
            if (ModelState.IsValid)
            {
                db.WellCastForecasts.Add(forecast);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, forecast);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = forecast.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Forecasts/5
        public HttpResponseMessage DeleteForecast(Guid id)
        {
            Forecast forecast = db.WellCastForecasts.Find(id);
            if (forecast == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastForecasts.Remove(forecast);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, forecast);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}