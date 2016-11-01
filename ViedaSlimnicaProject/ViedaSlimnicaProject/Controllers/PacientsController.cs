using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViedaSlimnicaProject.Models;
using ViedaSlimnicaProject.ViewModel;

namespace ViedaSlimnicaProject.Controllers
{
    public class PacientsController : Controller
    {
        private SmartHospitalDatabaseContext db = new SmartHospitalDatabaseContext();
        // GET: Pacients
        public ActionResult Index()
        {
            return View(db.Pacienti.ToList());
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
            var listOfRoomsToSelectFrom = new List<SelectListItem>();
            foreach (var room in db.Palatas.ToList())
            {
                var selection = new SelectListItem();
                if (room.PalatasIetilpiba < room.Pacienti.Count)
                {
                    // if the room is full
                    selection.Disabled = true;
                }

                selection.Text = "Palāta #" + room.PalatasID;
                selection.Value = room.PalatasID.ToString();
                listOfRoomsToSelectFrom.Add(selection);
            }
            var patientEditVm = new PacientsEditViewModel()
            {
                RoomsFromWhichToSelect = listOfRoomsToSelectFrom
            };
            

            return View(patientEditVm);
        }

        // POST: Pacients/Create
        [HttpPost]
        public ActionResult Create(PacientsEditViewModel pacients)
        {
            try
            {
                pacients.Patient.Palata = db.Palatas.Find(pacients.SelectedRoomId);
               // pacients.Patient.Nodala = pacients.Patient.Palata.Nodala;
                if (ModelState.IsValid)
                {
                    db.Pacienti.Add(pacients.Patient);
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
            
            var listOfRoomsToSelectFrom = new List<SelectListItem>();
            foreach (var room in db.Palatas.ToList())
            {
                var selection = new SelectListItem();
                if (room.PalatasIetilpiba < room.Pacienti.Count)
                {
                    // if the room is full
                    selection.Disabled = true;
                }
                
                selection.Text = "Palāta #" + room.PalatasID;
                selection.Value = room.PalatasID.ToString();
                listOfRoomsToSelectFrom.Add(selection);
            }
            var patientEditVm = new PacientsEditViewModel()
            {
                Patient = pacients,
                RoomsFromWhichToSelect = listOfRoomsToSelectFrom
            };

            if (pacients.Palata != null)
            {
                patientEditVm.SelectedRoomId = pacients.Palata.PalatasID;
            }

            return View(patientEditVm);
        }

        // POST: Pacients/Edit/5
        [HttpPost]
        public ActionResult Edit(PacientsEditViewModel patientEditVm)
        {
            try
            {

                var selectedRoom = db.Palatas.Single(room => room.PalatasID == patientEditVm.SelectedRoomId);
                patientEditVm.Patient.Palata = selectedRoom;
                //patientEditVm.Patient.Nodala = selectedRoom.Nodala;
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Palatas.Attach(selectedRoom);
                    db.Entry(patientEditVm.Patient).State = EntityState.Modified;
                    db.Entry(selectedRoom).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(patientEditVm);
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
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                Pacients pacients = db.Pacienti.Find(id);
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (pacients == null)
                    {
                        return HttpNotFound();
                    }
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
