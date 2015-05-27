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
    public class PunishersController : Controller
    {
        private ConnectionStringHellDB db = new ConnectionStringHellDB();

        // GET: Punishers
        public ActionResult Index()
        {
            var punishers = db.Punishers.Include(p => p.PunisherRank);
            return View(punishers.ToList());
        }

        // GET: Punishers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punisher punisher = db.Punishers.Find(id);
            if (punisher == null)
            {
                return HttpNotFound();
            }
            return View(punisher);
        }

        // GET: Punishers/Create
        public ActionResult Create()
        {
            ViewBag.PunisherRankId = new SelectList(db.PunisherRanks, "Id", "Name");
            return View();
        }

        // POST: Punishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,PunisherRankId")] Punisher punisher)
        {
            if (ModelState.IsValid)
            {
                db.Punishers.Add(punisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PunisherRankId = new SelectList(db.PunisherRanks, "Id", "Name", punisher.PunisherRankId);
            return View(punisher);
        }

        // GET: Punishers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punisher punisher = db.Punishers.Find(id);
            if (punisher == null)
            {
                return HttpNotFound();
            }
            ViewBag.PunisherRankId = new SelectList(db.PunisherRanks, "Id", "Name", punisher.PunisherRankId);
            return View(punisher);
        }

        // POST: Punishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,PunisherRankId")] Punisher punisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(punisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PunisherRankId = new SelectList(db.PunisherRanks, "Id", "Name", punisher.PunisherRankId);
            return View(punisher);
        }

        // GET: Punishers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punisher punisher = db.Punishers.Find(id);
            if (punisher == null)
            {
                return HttpNotFound();
            }
            return View(punisher);
        }

        // POST: Punishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Punisher punisher = db.Punishers.Find(id);
            db.Punishers.Remove(punisher);
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
