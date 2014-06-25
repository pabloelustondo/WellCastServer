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
            var musers = mdb.GetCollection("users").FindAll();
            foreach (var muser in musers) {
                User wuser = new User();
                var muserID = muser["_id"].ToString();
                wuser.ID = muser["_id"].ToString();
                wuser.Name = muser["username"].ToString();
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