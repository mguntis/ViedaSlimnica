using System;
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
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ViedaSlimnicaProject.Controllers
{
    public class PacientsController : Controller
    {

        public static string strKey = "U2A9/R*41FD412+4-123";

        public static string Encrypt(string strData)
        {
            string strValue = "";
            if (!string.IsNullOrEmpty(strKey))
            {
                if (strKey.Length < 16)
                {
                    char c = "XXXXXXXXXXXXXXXX"[16];
                    strKey = strKey + strKey.Substring(0, 16 - strKey.Length);
                }

                if (strKey.Length > 16)
                {
                    strKey = strKey.Substring(0, 16);
                }

                // create encryption keys
                byte[] byteKey = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
                byte[] byteVector = Encoding.UTF8.GetBytes(strKey.Substring(strKey.Length - 8, 8));

                // convert data to byte array
                byte[] byteData = Encoding.UTF8.GetBytes(strData);

                // encrypt 
                DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
                MemoryStream objMemoryStream = new MemoryStream();
                CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateEncryptor(byteKey, byteVector), CryptoStreamMode.Write);
                objCryptoStream.Write(byteData, 0, byteData.Length);
                objCryptoStream.FlushFinalBlock();

                // convert to string and Base64 encode
                strValue = Convert.ToBase64String(objMemoryStream.ToArray());
            }
            else
            {
                strValue = strData;
            }

            return strValue;
        }
        public static string Decrypt(string strData)
        {
            string strValue = "";
            if (!string.IsNullOrEmpty(strKey))
            {
                // convert key to 16 characters for simplicity
                if (strKey.Length < 16)
                {
                    strKey = strKey + strKey.Substring(0, 16 - strKey.Length);

                }

                if (strKey.Length > 16)
                {
                    strKey = strKey.Substring(0, 16);

                }

                // create encryption keys
                byte[] byteKey = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
                byte[] byteVector = Encoding.UTF8.GetBytes(strKey.Substring(strKey.Length - 8, 8));

                // convert data to byte array and Base64 decode
                byte[] byteData = new byte[strData.Length + 1];
                try
                {
                    byteData = Convert.FromBase64String(strData);
                }
                catch
                {
                    strValue = strData;
                }


                if (string.IsNullOrEmpty(strValue))
                {
                    // decrypt
                    DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
                    MemoryStream objMemoryStream = new MemoryStream();
                    CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Write);
                    objCryptoStream.Write(byteData, 0, byteData.Length);
                    objCryptoStream.FlushFinalBlock();

                    // convert to string
                    System.Text.Encoding objEncoding = System.Text.Encoding.UTF8;
                    strValue = objEncoding.GetString(objMemoryStream.ToArray());

                }
            }
            else
            {
                strValue = strData;
            }

            return strValue;
        }

        public static string HashSaltStore(string password)
        {
            // hash and salt, izveidojam hasho ko glabat
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 5000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
        public static bool HashSaltVerify(string password, string dbpassword)
        {
            // parbauda vai ievadiita parole ir pareiza
            byte[] hashBytes = Convert.FromBase64String(dbpassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 5000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i]) return false;
            return true;
        }
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
            var finduser = db.Pacienti.Find(id);
            var user = db.Accounts.Where(a => a.Patient.PacientaID == finduser.PacientaID).FirstOrDefault();
            if (id == null || user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var profils = db.Accounts.Find(user.ProfileID);
            profils.Password = Decrypt(profils.Password);
            var pacients = new DetailsView()
            {
                Pacients = db.Pacienti.Find(id),
                Profils = profils
            };
            pacients.Pacients.Palata = finduser.Palata;
            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // GET: Pacients/PatientView
        [Authorize(Roles ="User")]
        public ActionResult PatientView(int id)
        {
            var user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            var msglist = db.Zinojumi
                .Where(a => a.msgTo.PacientaID == user.Patient.PacientaID || a.msgTo.Vards == null)
                .ToList();
            var pacients = new PacientsView() {
                Pacients = db.Pacienti.Find(user.Patient.PacientaID),
                Msg = msglist.OrderByDescending(e => e.date)
            };

            if (pacients == null)
                return HttpNotFound();
            return View(pacients);
        }

        // GET: Pacients/Create
        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpGet]
        public ActionResult Create(int? roomID)
        {
            var patientEditVm = new PacientsEditViewModel();
            if (roomID == null) {
                patientEditVm.RoomsFromWhichToSelect = availableRooms();
            }
            else
            {
                patientEditVm.SelectedRoomId = roomID.GetValueOrDefault();
                patientEditVm.RoomsFromWhichToSelect = availableRooms();
                
            }
                return View(patientEditVm);
        }
        //Random function
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // POST: Pacients/Create
        [HttpPost]
        public ActionResult Create(PacientsEditViewModel pacients)
        {
            try
            {
                var selectedRoom = db.Palatas.Find(pacients.SelectedRoomId);
                pacients.Patient.Palata = selectedRoom;

                var password = RandomString(6);
                var insertprofile = new Profils
                {
                    Patient = pacients.Patient,
                    UserName = pacients.Patient.Epasts,
                    Password = Encrypt(password),
                    RoleStart = "User"
                };
                if (selectedRoom.PalatasIetilpiba <= selectedRoom.Pacienti.Count())
                {
                    // mēģinājums ievietot pilnā palātā vēlvienu pacientu
                    pacients.RoomsFromWhichToSelect = availableRooms();
                    ModelState.AddModelError("SelectedRoomId", "Palāta jau ir pilna!");
                    return View(pacients);
                }
                if (pacients.Patient.IerasanasDatums>DateTime.Now) {
                    // mēģinājums ievietot nākotnes datumu
                    pacients.RoomsFromWhichToSelect = availableRooms();
                    ModelState.AddModelError("Patient.IerasanasDatums", "Ievietots nākotnes datums!");
                    return View(pacients);
                }
                if (pacients.Patient.IerasanasDatums < DateTime.Now.AddDays(-3))
                {
                    // Mēģinājums ievietot pacientu palātā ar vairāk kā 3 dienu kavēšanos
                    pacients.RoomsFromWhichToSelect = availableRooms();
                    ModelState.AddModelError("Patient.IerasanasDatums", "Nokavētas vairāk kā 3 dienas!");
                    return View(pacients);
                }
                if (ModelState.IsValid)
                {
                    db.Pacienti.Add(pacients.Patient);
                    db.Accounts.Add(insertprofile);
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
        
        [HttpPost]
        public ActionResult NewMsg(string text)
        {
            Zinojumi message = new Zinojumi();
            Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            message.msg = text;
            message.profils = user;
            message.date = DateTime.Now;
            message.dateString = message.date.ToString("d MMM HH:mm");
            if (ModelState.IsValid)
            {
                db.Zinojumi.Add(message);
                db.SaveChanges();
                return RedirectToAction("Zinojumi");
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

                var finduser = db.Pacienti.Find(id);
                var user = db.Accounts.Where(a => a.Patient.PacientaID == finduser.PacientaID).FirstOrDefault();
                Pacients pacients = db.Pacienti.Find(id);
                Profils profile = db.Accounts.Find(user.ProfileID);

                var userMsgs = db.Zinojumi.Where(a => a.msgTo.PacientaID == finduser.PacientaID).ToList();
                    foreach (var i in userMsgs)
                    {
                        db.Zinojumi.Remove(i);
                    }
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
                    db.Accounts.Remove(profile);
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
            if (User.Identity.Name != "")
            {
                var user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
                switch (user.RoleStart)
                {
                    case "User":
                        return RedirectToAction("PatientView", new { id = user.Patient.PacientaID });
                    case "Employee":
                    case "SuperAdmin":
                        return RedirectToAction("Index");
                }

            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAc(Profils log, string returnUrl)
        {
            var user = db.Accounts.Where(a => a.UserName == log.UserName).FirstOrDefault();
            if (Decrypt(user.Password) == log.Password  && user!=null)
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
        public ActionResult SendMsgToPatient(int patientID, string MsgToPatient)
        {
            Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            var message = new Zinojumi() {
                msg = MsgToPatient,
                profils = user,
                date = DateTime.Now,
                dateString = DateTime.Now.ToString("d MMM HH:mm"),
                msgTo = db.Pacienti.Find(patientID)
        };
            db.Zinojumi.Add(message);
            db.SaveChanges();
           
            return RedirectToAction("Index");
        }
        

        [Authorize(Roles = "SuperAdmin,Employee")]
        public ActionResult Zinojumi()
        {
            var msgList = db.Zinojumi.ToList().OrderByDescending(e => e.date);
          //      msgList.OrderByDescending(e => e.date);
            return View(msgList);
        }

        [Authorize(Roles = "SuperAdmin, Employee")]
        public ActionResult DeleteMsg(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Zinojumi zinojums = db.Zinojumi.Find(id);
            if (zinojums == null)
                return HttpNotFound();
            return View(zinojums);
        }

        // POST: 
        [HttpPost, ActionName("DeleteMsg")]
        public ActionResult DeleteMsgConfirm(int? id)
        {
            try
            {
                Zinojumi zinojums = db.Zinojumi.Find(id);
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (zinojums == null)
                    {
                        return HttpNotFound();
                    }
                    db.Zinojumi.Remove(zinojums);
                    db.SaveChanges();
                    return RedirectToAction("Zinojumi");
                }
                return View(zinojums);
            }
            catch
            {
                return View();
            }

        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Employee")]
        public ActionResult EditMsg(int? id)
        {
            return View(db.Zinojumi.Find(id));
        }
        [HttpPost,ActionName("EditMsg")]
        public ActionResult EditMsgConfirm(Zinojumi zinojumi)
        {
            db.Entry(zinojumi).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Zinojumi");
        }
        [HttpGet]
        //[Authorize(Roles = "SuperAdmin")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost,ActionName("Register")]
        public ActionResult RegisterConfirm (Profils userData)
        {
            var userTest = db.Accounts.Where(a => a.UserName == userData.UserName).FirstOrDefault();
            if (userTest != null)
            {
                // users ar šādu username jau atrodas db
                ModelState.AddModelError("", "Lietotājs " + userData.UserName + " jau atrodas sistēmā!");
                ModelState.Remove("Password");
                return View();
            }
            var newProfile = new Profils()
            {
                UserName = userData.UserName,
                Password = Encrypt(userData.Password),
                RoleStart = userData.RoleStart,
                Vards = userData.Vards,
                Uzvards = userData.Uzvards
            };
            db.Accounts.Add(newProfile);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "Lietotājs izveidots";
            return View();
        }
    }
}
