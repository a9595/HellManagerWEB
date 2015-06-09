using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HellManagerWEB.Models;

namespace HellManagerWEB.Controllers
{
    public class SinsController : Controller
    {
        private ConnectionStringHellDB db = new ConnectionStringHellDB();

        // GET: Sins
        public ActionResult Index()
        {
            var sins = db.Sins.Include(s => s.Punisher).Include(s => s.Punishment).Include(s => s.SinDegree);
            return View(sins.ToList());
        }

        // GET: Sins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sin sin = db.Sins.Find(id);
            if (sin == null)
            {
                return HttpNotFound();
            }
            return View(sin);
        }

        // GET: Sins/Create
        public ActionResult Create()
        {
            ViewBag.PunisherId = new SelectList(db.Punishers, "Id", "Name");
            ViewBag.PunishmentId = new SelectList(db.Punishments, "Id", "Name");
            ViewBag.SinDegreeId = new SelectList(db.SinDegrees, "Id", "Name");
            return View();
        }

        // POST: Sins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PunishmentId,PunisherId,SinDegreeId")] Sin sin)
        {
            if (ModelState.IsValid)
            {
                db.Sins.Add(sin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PunisherId = new SelectList(db.Punishers, "Id", "Name", sin.PunisherId);
            ViewBag.PunishmentId = new SelectList(db.Punishments, "Id", "Name", sin.PunishmentId);
            ViewBag.SinDegreeId = new SelectList(db.SinDegrees, "Id", "Name", sin.SinDegreeId);
            return View(sin);
        }

        // GET: Sins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sin sin = db.Sins.Find(id);
            if (sin == null)
            {
                return HttpNotFound();
            }
            ViewBag.PunisherId = new SelectList(db.Punishers, "Id", "Name", sin.PunisherId);
            ViewBag.PunishmentId = new SelectList(db.Punishments, "Id", "Name", sin.PunishmentId);
            ViewBag.SinDegreeId = new SelectList(db.SinDegrees, "Id", "Name", sin.SinDegreeId);
            return View(sin);
        }

        // POST: Sins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PunishmentId,PunisherId,SinDegreeId")] Sin sin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PunisherId = new SelectList(db.Punishers, "Id", "Name", sin.PunisherId);
            ViewBag.PunishmentId = new SelectList(db.Punishments, "Id", "Name", sin.PunishmentId);
            ViewBag.SinDegreeId = new SelectList(db.SinDegrees, "Id", "Name", sin.SinDegreeId);
            return View(sin);
        }

        // GET: Sins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sin sin = db.Sins.Find(id);
            if (sin == null)
            {
                return HttpNotFound();
            }
            return View(sin);
        }

        // POST: Sins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sin sin = db.Sins.Find(id);
            db.Sins.Remove(sin);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
