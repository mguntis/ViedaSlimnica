using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViedaSlimnicaProject.Models;
using ViedaSlimnicaProject.ViewModel;

namespace ViedaSlimnicaProject.Controllers
{
    public class MessageController : Controller
    {
        private SmartHospitalDatabaseContext db = new SmartHospitalDatabaseContext();


        // GET: Message
        public ActionResult Index()
        {
            Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            var msgListReceived = db.Zinojumi.ToList()
                .Where(a => a.msgTo == user)
                .OrderByDescending(e => e.date);
            var msgListSent = db.Zinojumi.ToList()
                .Where(a => a.msgFrom == user)
                .OrderByDescending(e => e.date);
            var Messages = new ZinojumiView()
            {
                Received = msgListReceived,
                Sent = msgListSent
            };

            if (user.RoleStart == "User")
            {   // Pacienti
                var recipentList = db.Accounts.ToList()
                    .Where(a => a.RoleStart != "User" && a.ProfileID != user.ProfileID);
                Messages.AvailableRecipents = recipentList;
            }
            else
            {   // Ārsti/Admini
                //Var nosūtīt ziņu visiem admini/ārsti/pacienti
                var recipentList = db.Accounts.ToList()
                    .Where(a => a.ProfileID != user.ProfileID);
                Messages.AvailableRecipents = recipentList;
            }
            return View(Messages);
        }

        // POST: Message visiem
        [HttpPost]
        public ActionResult NewMsg(string text, string subject, int? msgTo)
        {
            Zinojumi message = new Zinojumi();
            Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            if (msgTo == null) return RedirectToAction("Index");
            if (text == "") return RedirectToAction("Index");
            message.msg = text;
            message.msgFrom = user;
            message.date = DateTime.Now;
            message.dateString = message.date.ToString("d MMM HH:mm");
            message.subject = subject;
            if (msgTo != null)
            {
                message.msgTo = db.Accounts.Find(msgTo);
            }
            db.Zinojumi.Add(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteMsg(int? msgID)
        {
            if (msgID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zinojumi msg = db.Zinojumi.Find(msgID);
            db.Zinojumi.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult EditorReplyMsg(int? msgID,string mode, string subject, string text)
        {
            if (msgID == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (mode == "reply")
            {
                Profils user = db.Accounts.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
                var replyMsg = new Zinojumi()
                {
                    msgTo = db.Zinojumi.Find(msgID).msgFrom,
                    subject = subject,
                    msg = text,
                    msgFrom = user,
                    date = DateTime.Now
                };
                replyMsg.dateString = replyMsg.date.ToString("d MMM HH:mm");
                db.Zinojumi.Add(replyMsg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (mode == "edit")
            {
                var msg = db.Zinojumi.Find(msgID);
                msg.msg = text;
                msg.subject = subject;
                db.Entry(msg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View();
        }

    }
}