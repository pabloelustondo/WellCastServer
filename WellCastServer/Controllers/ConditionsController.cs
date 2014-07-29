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
    public class ConditionsController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Conditions
        public IEnumerable<Condition> GetConditions()
        {
             var conditions = db.WellCastConditions.AsEnumerable();
             return conditions;
        }

        // GET api/Conditions/5
        public Condition GetCondition(String id)
        {

            Guid gid = new Guid(id);
            Condition condition = db.WellCastConditions.Find(gid);
            return condition;
        }

        // PUT api/Conditions/5
        public HttpResponseMessage PutCondition(String id, Condition condition)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != condition.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(condition).State = EntityState.Modified;

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

        // POST api/Conditions
        public HttpResponseMessage PostCondition(Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.WellCastConditions.Add(condition);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, condition);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = condition.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Conditions/5
        public HttpResponseMessage DeleteCondition(Guid id)
        {
            Condition condition = db.WellCastConditions.Find(id);
            if (condition == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastConditions.Remove(condition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, condition);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}