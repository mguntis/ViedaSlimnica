using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViedaSlimnicaProject.Context;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.Controllers
{
    public class PacientsController : Controller
    {
        private PacientsContext db = new PacientsContext();
        // GET: Pacients
        public ActionResult Index()
        {
            return View(db.Pacienti.ToList());
        }
        public ActionResult Palata(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var pacients = from p in db.Pacienti
                           where id == p.PalatasID
                           select p ; 

            return View(pacients.ToList());
        }

        // GET: Pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pacients pacients = db.Pacienti.Find(id);
            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // GET: Pacients/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        [HttpPost]
        public ActionResult Create(Pacients pacients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Pacienti.Add(pacients);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pacients);
            }
            catch
            {
                return View();
            }
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pacients pacients = db.Pacienti.Find(id);
            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // POST: Pacients/Edit/5
        [HttpPost]
        public ActionResult Edit(Pacients pacients)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(pacients).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                  
                    return RedirectToAction("Index");
                }
                return View(pacients);
            }
            catch
            {
                return View();
            }
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pacients pacients = db.Pacienti.Find(id);
            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // POST: Pacients/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id,Pacients pacients )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    pacients = db.Pacienti.Find(id);
                    if (pacients == null)
                        return HttpNotFound();
                    db.Pacienti.Remove(pacients);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pacients);
            }
            catch
            {
                return View();
            }
        }
    }
}
