﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViedaSlimnicaProject.Models;
using ViedaSlimnicaProject.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace ViedaSlimnicaProject.Controllers
{
    public class PacientsController : Controller
    {
        public List<SelectListItem> availableRooms()
        {
            // atrodam visas palātas, kurās ir brīvas vietas
            var listOfRoomsToSelectFrom = new List<SelectListItem>();
            foreach (var room in db.Palatas.ToList())
            {
                var selection = new SelectListItem();
                if (room.PalatasIetilpiba <= room.Pacienti.Count)
                {
                    // if the room is full
                    selection.Disabled = true;
                }
                if (listOfRoomsToSelectFrom != null)
                {
                selection.Text = "Palāta #" + room.PalatasID;
                selection.Value = room.PalatasID.ToString();
                listOfRoomsToSelectFrom.Add(selection);
                }
            }
            return listOfRoomsToSelectFrom;
        }


        private SmartHospitalDatabaseContext db = new SmartHospitalDatabaseContext();
        // GET: Pacients
        //(Roles ="Admin")]
        [Authorize(Roles = "SuperAdmin, Employee")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            ViewBag.CurrentSort = sortOrder;
            //Pievienoju kaartosanu pec datuma, uzvarda un nodalas
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nod_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            //lapošana

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            var pacienti = from s in db.Pacienti
                           select s;

            //Mekleesana peec varda, uzvarda, personas koda, telefona
            if (!String.IsNullOrEmpty(searchString))
            {
                pacienti = pacienti.Where(s => s.Uzvards.Contains(searchString)
                                       || s.Vards.Contains(searchString) || s.TNumurs.Contains(searchString) || s.PersKods.Contains(searchString));
            }


            //Šī daļa ir prieķš kārtošanas
           switch (sortOrder)
            {
                case "nod_desc":
                    pacienti = pacienti.OrderByDescending(s => s.Palata.Nodala);
                    break;
                case "date":
                    pacienti = pacienti.OrderBy(s => s.IerasanasDatums);
                    break;
                case "date_desc":
                    pacienti = pacienti.OrderByDescending(s => s.IerasanasDatums);
                    break;
                default:
                    pacienti = pacienti.OrderBy(s => s.Uzvards);
                    break;
            }
            //lapošanas atributi
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pacienti.ToPagedList(pageNumber, pageSize));
            







        }
        [Authorize(Roles = "SuperAdmin, Employee")]
        public ActionResult Palata(int id)
        {
            

            return View(db.Pacienti.Where(q => q.Palata.PalatasID == id).Take(4).ToList());
        }

        // GET: Pacients/Details/5
        [Authorize(Roles = "SuperAdmin, Employee, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pacients pacients = db.Pacienti.Find(id);
            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // GET: Pacients/PatientView
        [Authorize(Roles ="User")]
        public ActionResult PatientView(int id)
        {
            var userid = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault().Patient.PacientaID;
            var msglist = db.Zinojumi.ToList();
            var pacients = new PacientsView() {
                Pacients = db.Pacienti.Find(userid),
                Msg = msglist.OrderByDescending(e => e.date)
            };

            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // GET: Pacients/Create
        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpGet]
        public ActionResult Create()
        {
            var patientEditVm = new PacientsEditViewModel()
            {
                RoomsFromWhichToSelect = availableRooms()
            };
                return View(patientEditVm);
        }

        // POST: Pacients/Create
        [HttpPost]
        public ActionResult Create(PacientsEditViewModel pacients)
        {
            try
            {
                var selectedRoom = db.Palatas.Find(pacients.SelectedRoomId);
                pacients.Patient.Palata = selectedRoom;
                if (selectedRoom.PalatasIetilpiba <= selectedRoom.Pacienti.Count())
                {
                    // mēģinājums ievietot pilnā palātā vēlvienu pacientu
                    pacients.RoomsFromWhichToSelect = availableRooms();
                    ModelState.AddModelError("SelectedRoomId", "Palāta jau ir pilna!");
                    return View(pacients);
                }
                if (ModelState.IsValid)
                {
                    db.Pacienti.Add(pacients.Patient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                // validācija parāda kļūdas
                // Atgriežam view, bet tā kā GET metode netiek palaista šajā gadījumā un šai metodei netika nodots
                // pieejamo palātu saraksts pievienojam tās atkal šim objektam/entitijai (nezinu kā pareizi viņu sauc)
                pacients.RoomsFromWhichToSelect = availableRooms();
                return View(pacients);
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Pacients/NewMsg
        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpGet]
        public ActionResult NewMsg(){
            return View();
        }
        [HttpPost]
        public ActionResult NewMsg(Zinojumi message){
            Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            message.profils = user;
            message.date = DateTime.Now;
            message.dateString = message.date.ToString("d MMM HH:mm");
            if (ModelState.IsValid)
            {
                db.Zinojumi.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Pacients/Edit/5
        [Authorize(Roles = "SuperAdmin, Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pacients pacients = db.Pacienti.Find(id);
            if (pacients == null)
                return HttpNotFound();
            var patientEditVm = new PacientsEditViewModel()
            {
                Patient = pacients,
                RoomsFromWhichToSelect = availableRooms()
            };

            if (pacients.Palata != null)
            {
                patientEditVm.SelectedRoomId = pacients.Palata.PalatasID;
                patientEditVm.Patient.Palata = pacients.Palata;
            }

            return View(patientEditVm);
        }

        // POST: Pacients/Edit/5
        [HttpPost]
        public ActionResult Edit(PacientsEditViewModel patientEditVm)
        {
            //var selectedRoom = db.Palatas.Single(room => room.PalatasID == patientEditVm.SelectedRoomId);
            //patientEditVm.Patient.Palata = selectedRoom;
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                //db.Palatas.Attach(selectedRoom);
                db.Entry(patientEditVm.Patient).State = EntityState.Modified;
                //db.Entry(selectedRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientEditVm);

        }

        // GET: Pacients/Delete/5
        [Authorize(Roles = "SuperAdmin, Employee")]
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
        public ActionResult LoginAc()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAc(Profils log, string returnUrl)
        {
            var user = db.Accounts.Where(a => a.UserName == log.UserName && a.Password == log.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                if (user.RoleStart == "Employee" || user.RoleStart == "SuperAdmin")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    int returnID = user.Patient.PacientaID;
                    if (ModelState.IsValid)
                    {
                        return RedirectToAction("PatientView", new { id = returnID });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Nepareiza parole vai lietotājvārds");
            }
            ModelState.Remove("Password");
            return View();
        }
        [Authorize]
        public ActionResult LogOf()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAc");
        }
    }
}
