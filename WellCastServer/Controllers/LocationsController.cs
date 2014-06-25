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
    public class LocationsController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Locations
        public WellCastEnvelope<IEnumerable<Location>> GetLocations()
        {
            var locations = db.WellCastLocations.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<Location>>(locations);
            return envelop;
        }

        // GET api/Locations/5
        public WellCastEnvelope<Location> GetLocation(String id)
        {
            WellCastEnvelope<Location> envelope;
            try
            {
                Guid gid = new Guid(id);
                Location location = db.WellCastLocations.Find(gid);
                envelope = new WellCastEnvelope<Location>(location);

                if (location == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<Location>(null);
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


        // PUT api/Locations/5
        public HttpResponseMessage PutWellCastLocation(String id, Location wellcastlocation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != wellcastlocation.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(wellcastlocation).State = EntityState.Modified;

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

        // POST api/Locations
        public HttpResponseMessage PostWellCastLocation(Location wellcastlocation)
        {
            if (ModelState.IsValid)
            {
                db.WellCastLocations.Add(wellcastlocation);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, wellcastlocation);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = wellcastlocation.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Locations/5
        public HttpResponseMessage DeleteWellCastLocation(Guid id)
        {
            Location wellcastlocation = db.WellCastLocations.Find(id);
            if (wellcastlocation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastLocations.Remove(wellcastlocation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, wellcastlocation);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}