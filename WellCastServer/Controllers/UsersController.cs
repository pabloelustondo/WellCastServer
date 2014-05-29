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
    public class UsersController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Users
        public WellCastEnvelope<IEnumerable<User>> GetUsers()
        {
            var conditions = db.WellCastUsers.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<User>>(conditions);
            return envelop;
        }

        // GET api/Users/5
        public WellCastEnvelope<User> GetUser(String id)
        {
            WellCastEnvelope<User> envelope;
            try
            {
                Guid gid = new Guid(id);
                User condition = db.WellCastUsers.Find(gid);
                envelope = new WellCastEnvelope<User>(condition);

                if (condition == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<User>(null);
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

        // PUT api/Users/5
        public HttpResponseMessage PutUser(Guid id, User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != user.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(user).State = EntityState.Modified;

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

        // POST api/Users
        public HttpResponseMessage PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.WellCastUsers.Add(user);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Users/5
        public HttpResponseMessage DeleteUser(Guid id)
        {
            User user = db.WellCastUsers.Find(id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastUsers.Remove(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}