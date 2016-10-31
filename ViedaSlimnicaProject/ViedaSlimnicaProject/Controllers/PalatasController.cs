using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Controllers
{
    public class PalatasController : Controller
    {
        private SmartHospitalDatabaseContext db = new SmartHospitalDatabaseContext();

        // GET: Palatas
        public ActionResult Index()
        {
            var rooms = db.Palatas.ToList();
            return View(rooms);
        }

        // GET: Palatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palata palata =  db.Palatas.Find(id);
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
        [HttpPost]
        public ActionResult Create(Palata palata)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    db.Palatas.Add(palata);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(palata);
            }
            catch {
                return View();
                    }
        }

        // GET: Palatas/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palata palata =  db.Palatas.Find(id);
            if (palata == null)
            {
                return HttpNotFound();
            }
            return View(palata);
        }

        // POST: Palatas/Edit/5
        [HttpPost]
        public ActionResult Edit(Palata palata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(palata).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(palata);
            }
            catch {
                return View();
            }
        }


        // GET: Palatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Palata palata = db.Palatas.Find(id);
            if (palata == null)
                return HttpNotFound();
            return View(palata);
        }

        // POST: Palatas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                Palata palata = db.Palatas.Find(id);
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (palata == null)
                    {
                        return HttpNotFound();
                    }
                    db.Palatas.Remove(palata);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(palata);
            }
            catch
            {
                return View();
            }
        }
    }
}
