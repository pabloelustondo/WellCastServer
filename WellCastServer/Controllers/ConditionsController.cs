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
        public WellCastEnvelope<IEnumerable<Condition>> GetConditions()
        {
             var conditions = db.WellCastConditions.AsEnumerable();
             var envelop = new WellCastEnvelope<IEnumerable<Condition>>(conditions);
             return envelop;
        }

        // GET api/Conditions/5
        public WellCastEnvelope<Condition> GetCondition(String id)
        {
            WellCastEnvelope<Condition> envelope;
            try { 
            Guid gid = new Guid(id);
            Condition condition = db.WellCastConditions.Find(gid);
            envelope = new WellCastEnvelope<Condition>(condition);

            if (condition == null)
            {
                envelope.meta.status = WellCastMessages.NonExistingId.code;
            }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<Condition>(null);
                if (e.Message.Contains("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")){
                    
                envelope.meta.status = WellCastMessages.InvalidId.code;
                envelope.meta.message = e.Message;
                
                }else{
                envelope.meta.status = WellCastMessages.Exception.code;
                envelope.meta.message = e.Message;
                    }
            }
            return envelope;
        }

        // PUT api/Conditions/5
        public HttpResponseMessage PutCondition(Guid id, Condition condition)
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