using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellCastServer.Models;

namespace WellCastServer.Controllers
{
    public class ProfileController : WellCastController
    {
        private WellCastServerContext db = new WellCastServerContext();

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            List<Profile> wprofiles = new List<Models.Profile>();
            var mprofiles = mdb.GetCollection("profiles").FindAll();
            foreach (var mprofile in mprofiles)
            {
                Profile wprofile = new Profile();
                try { wprofile.ID = mprofile["_id"].ToString(); } catch (Exception) { };
                try { wprofile.Name = mprofile["name"].ToString(); } catch (Exception) { };
                try { wprofile.Age = Convert.ToInt16(mprofile["age"].ToString()); } catch (Exception) { };
                try { wprofile.Gender = mprofile["gender"].ToString(); } catch (Exception) { };
                try { wprofile.UserID = mprofile["profile_id"].ToString(); } catch (Exception) { };
                try { 
                var conditions = mprofile["conditions"].AsBsonArray.ToList();
                wprofile.ConditionIDs = new List<string>();
                foreach (var condition in conditions) { wprofile.ConditionIDs.Add(condition.ToString()); }
                } catch (Exception) { };

                try
                {
                    var locations = mprofile["locations"].AsBsonArray.ToList();
                    wprofile.LocationIDs = new List<string>();
                    foreach (var location in locations) { wprofile.LocationIDs.Add(location.ToString()); }
                }
                catch (Exception) { };

                wprofiles.Add(wprofile);
            }
            return View(wprofiles);
        }

        //
        // GET: /Profile/Details/5

        public ActionResult Details(Guid id)
        {
            Profile profile = db.WellCastProfiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.WellCastUsers, "ID", "Name");
            return View();
        }

        //
        // POST: /Profile/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.ID = profile.Name;
                db.WellCastProfiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.WellCastUsers, "ID", "Name", profile.UserID);
            return View(profile);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(Guid id)
        {
            Profile profile = db.WellCastProfiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.WellCastUsers, "ID", "Name", profile.UserID);
            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.WellCastUsers, "ID", "Name", profile.UserID);
            return View(profile);
        }

        //
        // GET: /Profile/Delete/5

        public ActionResult Delete(Guid id)
        {
            Profile profile = db.WellCastProfiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Profile profile = db.WellCastProfiles.Find(id);
            db.WellCastProfiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}