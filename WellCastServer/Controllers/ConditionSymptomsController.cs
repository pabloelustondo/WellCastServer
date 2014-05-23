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
    public class ConditionSymptomsController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/ConditionSymptoms
        public IEnumerable<ConditionSymptom> GetConditionSymptoms()
        {
            var wellcastconditionsymptoms = db.WellCastConditionSymptoms.Include(c => c.Condition).Include(c => c.Symptom);
            return wellcastconditionsymptoms.AsEnumerable();
        }

        // GET api/ConditionSymptoms/5
        public ConditionSymptom GetConditionSymptom(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            if (conditionsymptom == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return conditionsymptom;
        }

        // PUT api/ConditionSymptoms/5
        public HttpResponseMessage PutConditionSymptom(Guid id, ConditionSymptom conditionsymptom)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != conditionsymptom.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(conditionsymptom).State = EntityState.Modified;

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

        // POST api/ConditionSymptoms
        public HttpResponseMessage PostConditionSymptom(ConditionSymptom conditionsymptom)
        {
            if (ModelState.IsValid)
            {
                db.WellCastConditionSymptoms.Add(conditionsymptom);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, conditionsymptom);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = conditionsymptom.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ConditionSymptoms/5
        public HttpResponseMessage DeleteConditionSymptom(Guid id)
        {
            ConditionSymptom conditionsymptom = db.WellCastConditionSymptoms.Find(id);
            if (conditionsymptom == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastConditionSymptoms.Remove(conditionsymptom);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, conditionsymptom);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}