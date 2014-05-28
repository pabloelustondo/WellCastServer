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
    public class ProfilesController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Profiles
        public WellCastEnvelope<IEnumerable<Profile>> GetProfiles()
        {
            var conditions = db.WellCastProfiles.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<Profile>>(conditions);
            return envelop;
        }

        // GET api/Profiles/5
        public WellCastEnvelope<Profile> GetProfile(String id)
        {
            WellCastEnvelope<Profile> envelope;
            try
            {
                Guid gid = new Guid(id);
                Profile condition = db.WellCastProfiles.Find(gid);
                envelope = new WellCastEnvelope<Profile>(condition);

                if (condition == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<Profile>(null);
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
        // PUT api/Profiles/5
        public HttpResponseMessage PutProfile(Guid id, Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != profile.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(profile).State = EntityState.Modified;

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

        // POST api/Profiles
        public HttpResponseMessage PostProfile(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.WellCastProfiles.Add(profile);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, profile);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = profile.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Profiles/5
        public HttpResponseMessage DeleteProfile(Guid id)
        {
            Profile profile = db.WellCastProfiles.Find(id);
            if (profile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastProfiles.Remove(profile);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, profile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}