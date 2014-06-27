using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

        // GET api/Forecasts
        public WellCastEnvelope<IEnumerable<Forecast>> GetForecasts()
        {
            var conditions = db.WellCastForecasts.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<Forecast>>(conditions);
            return envelop;
        }

        // GET api/Forecasts/5
        public WellCastEnvelope<Forecast> GetForecast(String id)
        {
            WellCastEnvelope<Forecast> envelope;
            try
            {
                Guid gid = new Guid(id);
                Forecast condition = db.WellCastForecasts.Find(gid);
                envelope = new WellCastEnvelope<Forecast>(condition);

                if (condition == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<Forecast>(null);
                if (e.Message.Contains("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"))
                {

                    envelope.meta.status = WellCastStatusList.InvalidId.code;
                    envelope.meta.message = e.Message;

                }
                else
                {
                    envelope.meta.status = WellCastStatusList.Exception.code;
                    envelope.meta.message = e.Message;
                }
            }
            return envelope;
        }

        // GET api/Forecasts/5
        public WellCastEnvelope<List<Forecast>> GetForecastsForUser(String userid)
        {
            WellCastEnvelope<List<Forecast>> envelope;
            try
            
            {
                String UserIdGuid = userid;
                List<Forecast> conditions = db.WellCastForecasts.Where(f => f.UserMID == UserIdGuid).ToList();
                envelope = new WellCastEnvelope<List<Forecast>>(conditions);

                if (conditions == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<List<Forecast>>(null);
                if (e.Message.Contains("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"))
                {

                    envelope.meta.status = WellCastStatusList.InvalidId.code;
                    envelope.meta.message = e.Message;

                }
                else
                {
                    envelope.meta.status = WellCastStatusList.Exception.code;
                    envelope.meta.message = e.Message;
                }
            }
            return envelope;
        }

        // GET api/Forecasts/5
        public WellCastEnvelope<List<Forecast>> GetForecastsForProfile(String profileid)
        {
            WellCastEnvelope<List<Forecast>> envelope;
            try
            {
                List<Forecast> conditions = db.WellCastForecasts.Where(f => f.ProfileMID == profileid).ToList();
                envelope = new WellCastEnvelope<List<Forecast>>(conditions);

                if (conditions == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<List<Forecast>>(null);
                if (e.Message.Contains("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"))
                {

                    envelope.meta.status = WellCastStatusList.InvalidId.code;
                    envelope.meta.message = e.Message;

                }
                else
                {
                    envelope.meta.status = WellCastStatusList.Exception.code;
                    envelope.meta.message = e.Message;
                }
            }
            return envelope;
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