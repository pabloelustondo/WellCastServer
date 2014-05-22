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
    public class SymptomsController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/Symptoms
        public IEnumerable<Symptom> GetSymptoms()
        {
            var wellcastsymptoms = db.WellCastSymptoms.Include(s => s.SymptomCategory);
            return wellcastsymptoms.AsEnumerable();
        }

        // GET api/Symptoms/5
        public Symptom GetSymptom(int id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            if (symptom == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return symptom;
        }

        // PUT api/Symptoms/5
        public HttpResponseMessage PutSymptom(int id, Symptom symptom)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != symptom.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(symptom).State = EntityState.Modified;

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

        // POST api/Symptoms
        public HttpResponseMessage PostSymptom(Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                db.WellCastSymptoms.Add(symptom);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, symptom);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = symptom.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Symptoms/5
        public HttpResponseMessage DeleteSymptom(int id)
        {
            Symptom symptom = db.WellCastSymptoms.Find(id);
            if (symptom == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastSymptoms.Remove(symptom);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, symptom);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}