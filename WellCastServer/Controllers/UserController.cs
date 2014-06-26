using MongoDB.Driver;
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
    public class UserController : WellCastController
    {
        private WellCastServerContext db = new WellCastServerContext();
        //
        // GET: /User/

        public ActionResult Index()
        {
            List<User> wusers = new List<Models.User>();
            var musers = mdb.GetCollection("userDatas").FindAll();
            var profiles = mdb.GetCollection("profiles").FindAll();
            Dictionary<String, Object> profilesDic = new Dictionary<string, object>();
            
            foreach (var profile in profiles) {
                profilesDic.Add(profile["_id"].ToString(), profile);
            }
         


            foreach (var muser in musers) {
                User wuser = new User();
                var muserID = muser["_id"].ToString();
                wuser.ID = muser["_id"].ToString();

                try
                {
                    var locations = muser["locations"].AsBsonArray.ToList();
                    wuser.Locations = new List<Location>();
                    foreach (var mlocation in locations) {
                        Location wlocation = new Location();
                        try { wlocation.Name = mlocation["name"].ToString(); } catch (Exception) { };
                        try { wlocation.Description = mlocation["description"].ToString(); } catch (Exception) { };

                        try { wlocation.lat = Convert.ToDouble(mlocation["latitude"].ToString()); } catch (Exception) { };
                        try { wlocation.lon = Convert.ToDouble(mlocation["longitude"].ToString()); } catch (Exception) { };

                        wuser.Locations.Add(wlocation); 
                    }
                }
                catch (Exception) { };

                try
                {
                    var profileIDs = muser["profiles"].AsBsonArray.ToList();
                    wuser.ProfileIDs = new List<string>();
                    wuser.Profiles = new List<Profile>();
                    foreach (var profileID in profileIDs) { 
                        wuser.ProfileIDs.Add(profileID.ToString());
                        dynamic mprofile;
                        profilesDic.TryGetValue(profileID.ToString(), out mprofile);
                        Profile wprofile = new Profile();
                        try { wprofile.ID = mprofile["_id"].ToString(); } catch (Exception) { };
                        try { wprofile.Name = mprofile["name"].ToString(); } catch (Exception) { };
                        try { wprofile.Gender = mprofile["gender"].ToString(); } catch (Exception) { };
                        try { wprofile.Age = Convert.ToInt16(mprofile["age"].ToString()); } catch (Exception) { };

                        try
                        {
                            var conditions = mprofile["conditions"].AsBsonArray.ToList();
                            wprofile.ConditionIDs = new List<string>();
                            foreach (var condition in conditions) { wprofile.ConditionIDs.Add(condition.ToString()); }
                        } catch (Exception) { };


                        wuser.Profiles.Add(wprofile); 
                    }
                }

                catch (Exception) { };

                wusers.Add(wuser);
            }
            return View(wusers);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(Guid id)
        {
            User user = db.WellCastUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.ID = user.Name;
                db.WellCastUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(Guid id)
        {
            User user = db.WellCastUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(Guid id)
        {
            User user = db.WellCastUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.WellCastUsers.Find(id);
            db.WellCastUsers.Remove(user);
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