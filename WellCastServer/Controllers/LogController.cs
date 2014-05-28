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
    public class LogController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Log
        public IEnumerable<WellCastLog> GetWellCastLogs()
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.Day + "-" +  now.Hour + "-" + now.Minute + "-" + now.Second;
            WellCastLog wellcastlog = new WellCastLog();
            wellcastlog.Label = "label" + timeStamp;
            wellcastlog.Label = "message" + timeStamp;
            wellcastlog.timeStamp = now;
            db.WellCastLogs.Add(wellcastlog);
            db.SaveChanges();
            return db.WellCastLogs.AsEnumerable();
        }

        // GET api/Log/5
        public WellCastLog GetWellCastLog(int id)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            if (wellcastlog == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return wellcastlog;
        }

        // PUT api/Log/5
        public HttpResponseMessage PutWellCastLog(int id, WellCastLog wellcastlog)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != wellcastlog.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(wellcastlog).State = EntityState.Modified;

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

        // POST api/Log
        public HttpResponseMessage PostWellCastLog(WellCastLog wellcastlog)
        {
            if (ModelState.IsValid)
            {
                db.WellCastLogs.Add(wellcastlog);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, wellcastlog);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = wellcastlog.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Log/5
        public HttpResponseMessage DeleteWellCastLog(int id)
        {
            WellCastLog wellcastlog = db.WellCastLogs.Find(id);
            if (wellcastlog == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastLogs.Remove(wellcastlog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, wellcastlog);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}