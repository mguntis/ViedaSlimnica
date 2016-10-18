using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViedaSlimnicaProject.Context;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Controllers
{
    public class PalatasController : Controller
    {
        private PalataContext db = new PalataContext();

        // GET: Palatas
        public ActionResult Index()
        {
            return View(db.Palatas.ToList());
        }

        // GET: Palatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palata palata = db.Palatas.Find(id);
            if (palata == null)
            {
                return HttpNotFound();
            }
            return View(palata);
        }

        // GET: Palatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Palatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PalatasID,Nodala,Stavs,PalatasIetilpiba,GultasNr")] Palata palata)
        {
            if (ModelState.IsValid)
            {
                db.Palatas.Add(palata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(palata);
        }

        // GET: Palatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palata palata = db.Palatas.Find(id);
            if (palata == null)
            {
                return HttpNotFound();
            }
            return View(palata);
        }

        // POST: Palatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PalatasID,Nodala,Stavs,PalatasIetilpiba,GultasNr")] Palata palata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(palata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(palata);
        }

        // GET: Palatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palata palata = db.Palatas.Find(id);
            if (palata == null)
            {
                return HttpNotFound();
            }
            return View(palata);
        }

        // POST: Palatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Palata palata = db.Palatas.Find(id);
            db.Palatas.Remove(palata);
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
