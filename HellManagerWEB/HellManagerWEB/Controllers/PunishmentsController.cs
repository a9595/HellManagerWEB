using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HellManagerWEB.Models;

namespace HellManagerWEB.Controllers
{
    public class PunishmentsController : Controller
    {
        private ConnectionStringHellDB db = new ConnectionStringHellDB();

        // GET: Punishments
        public ActionResult Index()
        {
            return View(db.Punishments.ToList());
        }

        // GET: Punishments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punishment punishment = db.Punishments.Find(id);
            if (punishment == null)
            {
                return HttpNotFound();
            }
            return View(punishment);
        }

        // GET: Punishments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Punishments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TreatCycle")] Punishment punishment)
        {
            if (ModelState.IsValid)
            {
                db.Punishments.Add(punishment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(punishment);
        }

        // GET: Punishments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punishment punishment = db.Punishments.Find(id);
            if (punishment == null)
            {
                return HttpNotFound();
            }
            return View(punishment);
        }

        // POST: Punishments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TreatCycle")] Punishment punishment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(punishment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(punishment);
        }

        // GET: Punishments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punishment punishment = db.Punishments.Find(id);
            if (punishment == null)
            {
                return HttpNotFound();
            }
            return View(punishment);
        }

        // POST: Punishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Punishment punishment = db.Punishments.Find(id);
            db.Punishments.Remove(punishment);
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
