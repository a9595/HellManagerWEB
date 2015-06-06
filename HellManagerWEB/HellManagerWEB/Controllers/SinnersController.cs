using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HellManagerWEB.Models;

namespace HellManagerWEB.Controllers
{
    public class SinnersController : Controller
    {
        private ConnectionStringHellDB db = new ConnectionStringHellDB();

        // GET: Sinners
        public ActionResult Index()
        {
            var sinners = db.Sinners.Include(s => s.Gender);
            return View(sinners.ToList());
        }

        // GET: Sinners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinner sinner = db.Sinners.Find(id);
            if (sinner == null)
            {
                return HttpNotFound();
            }
            return View(sinner);
        }

        // GET: Sinners/Create
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            return View();
        }


        // POST: Sinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sinner sinner)
        {
            if (ModelState.IsValid)
            {
                db.Sinners.Add(sinner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", sinner.GenderId);
            return View(sinner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithSins(SinnerWithSins sinnerWithSins)
        {
            if (ModelState.IsValid)
            {
                List<Sin> sinnerSins = new List<Sin>(db.Sins.Where(sin => sinnerWithSins.SinsSelectedIndexes.Contains(sin.Id)));
                sinnerWithSins.Sinner.Sins = sinnerSins;
                sinnerWithSins.Sinner.GenderId = sinnerWithSins.SelectedGenderId;
                sinnerWithSins.Sinner.Gender = db.Genders.First(gender => sinnerWithSins.SelectedGenderId == gender.Id);

                db.Sinners.Add(sinnerWithSins.Sinner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", sinnerWithSins.Sinner.GenderId);
            return View(sinnerWithSins);
        }

//        // GET: Sinners/Create
//        public ActionResult CreateWithSins()
//        {
//            SinnerWithSins sinnerWithSins = new SinnerWithSins
//            {
//                Sins = new List<Sin>(db.Sins)
//            };
//
//            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
//            return View(sinnerWithSins);
//        }

        // GET: Sinners/Create
        public ActionResult CreateWithSins()
        {
            SinnerWithSins sinnerWithSins = new SinnerWithSins
            {
                Sins = new List<Sin>(db.Sins),
                Gender = new List<Gender>(db.Genders)
            };

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            return View(sinnerWithSins);
        }

       
        // GET: Sinners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinner sinner = db.Sinners.Find(id);
            if (sinner == null)
            {
                return HttpNotFound();
            }
            //TODO: figure it out
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", sinner.GenderId);
            return View(sinner);
        }


        // POST: Sinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Age,JobTitle,Salary,GenderId")] Sinner sinner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", sinner.GenderId);
            return View(sinner);
        }

        // GET: Sinners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinner sinner = db.Sinners.Find(id);
            if (sinner == null)
            {
                return HttpNotFound();
            }
            return View(sinner);
        }

        // POST: Sinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sinner sinner = db.Sinners.Find(id);
            List<Sin> sinnerSins = new List<Sin>(sinner.Sins);

            foreach (Sin sin in sinnerSins)
            {
                db.usp_SinnerSinDelete(sinner.Id, sin.Id);
                db.SaveChanges();

            }

            db.Sinners.Remove(sinner);


            try
            {
                db.SaveChanges();
            }
            catch (Exception EX_NAME)
            {
                db.Sinners.Remove(sinner);
            }
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
