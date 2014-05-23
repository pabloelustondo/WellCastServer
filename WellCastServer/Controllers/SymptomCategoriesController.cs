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
    public class SymptomCategoriesController : ApiController
    {
        private WellCastServerContext db = new WellCastServerContext();

        // GET api/SymptomCategories
        public IEnumerable<SymptomCategory> GetSymptomCategories()
        {
            return db.WellCastSymptomCategories.AsEnumerable();
        }

        // GET api/SymptomCategories/5
        public SymptomCategory GetSymptomCategory(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            if (symptomcategory == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return symptomcategory;
        }

        // PUT api/SymptomCategories/5
        public HttpResponseMessage PutSymptomCategory(Guid id, SymptomCategory symptomcategory)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != symptomcategory.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(symptomcategory).State = EntityState.Modified;

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

        // POST api/SymptomCategories
        public HttpResponseMessage PostSymptomCategory(SymptomCategory symptomcategory)
        {
            if (ModelState.IsValid)
            {
                db.WellCastSymptomCategories.Add(symptomcategory);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, symptomcategory);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = symptomcategory.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SymptomCategories/5
        public HttpResponseMessage DeleteSymptomCategory(Guid id)
        {
            SymptomCategory symptomcategory = db.WellCastSymptomCategories.Find(id);
            if (symptomcategory == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.WellCastSymptomCategories.Remove(symptomcategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, symptomcategory);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}