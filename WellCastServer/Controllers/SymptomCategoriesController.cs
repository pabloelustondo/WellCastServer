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
        public WellCastEnvelope<IEnumerable<SymptomCategory>> GetSymptomCategories()
        {
            var symptomCategories = db.WellCastSymptomCategories.AsEnumerable();
            var envelop = new WellCastEnvelope<IEnumerable<SymptomCategory>>(symptomCategories);
            return envelop;
        }

        // GET api/SymptomCategories/5
        public WellCastEnvelope<SymptomCategory> GetSymptomCategory(String id)
        {
            WellCastEnvelope<SymptomCategory> envelope;
            try
            {
                Guid gid = new Guid(id);
                SymptomCategory symptomCategory = db.WellCastSymptomCategories.Find(gid);
                envelope = new WellCastEnvelope<SymptomCategory>(symptomCategory);

                if (symptomCategory == null)
                {
                    envelope.meta.status = WellCastStatusList.NonExistingId.code;
                }

            }
            catch (Exception e)
            {
                envelope = new WellCastEnvelope<SymptomCategory>(null);
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

        // PUT api/SymptomCategories/5
        public HttpResponseMessage PutSymptomCategory(String id, SymptomCategory symptomcategory)
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