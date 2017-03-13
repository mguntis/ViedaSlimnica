using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ViedaSlimnicaProject.Hubs
{
public class UserConnection
    {
        public string UserName { set; get; }
        public string ConnectionID { set; get; }
    }

public class MessagesHub : Hub
    {
        public static List<UserConnection> uList = new List<UserConnection>();
        private SmartHospitalDatabaseContext db = new SmartHospitalDatabaseContext();
        public void Hello()
        {
            Clients.All.hello();
        }
        public void MarkMessage(int? msgID)
        {
            if (msgID != null)
            {
                db.Zinojumi.Find(msgID).isRead = true;
                db.SaveChanges();
                var user = db.Accounts.Where(a => a.UserName == Context.User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    var newMessages = db.Zinojumi.Where(a => a.msgTo.ProfileID == user.ProfileID && !a.isRead).Count();
                    if (newMessages >= 0)
                    {
                        var client = uList.Where(o => o.UserName == user.UserName).FirstOrDefault();
                        Clients.Client(client.ConnectionID).UpdateMessageCount(newMessages);
                    }
                }
            }
        }
        public override Task OnConnected()
        {
            var us = new UserConnection();
            us.UserName = Context.User.Identity.Name;
            us.ConnectionID = Context.ConnectionId;
            var userinList = uList.Where(a => a.UserName == us.UserName).FirstOrDefault();
            if (userinList == null)
            {
                uList.Add(us);
            }
            else
            {
                userinList.ConnectionID = us.ConnectionID;
            }
            return base.OnConnected();
        }

        public void SendNotification(int? userID)
        {
            //Clients.All.receiveNotification("hello");
                
            if (userID != null)
            {
                var sender = db.Accounts.Where(a => a.UserName == Context.User.Identity.Name).FirstOrDefault();
                var userProfile = db.Accounts.Find(userID);
                var user = uList.Where(o => o.UserName == userProfile.UserName).FirstOrDefault();
                if (user != null)
                {
                    var newMsgCount = db.Zinojumi.Where(a => a.msgTo.ProfileID == userProfile.ProfileID && !a.isRead).Count();
                    string message = "Jums ir jauna ziņa no " + sender.Vards + " " + sender.Uzvards;
                    Clients.Client(user.ConnectionID).receiveNotification(message,newMsgCount+1);
                    //Clients.All.receiveNotification(message);
                }
                }
            }
        public void SendNotificationReply(int? msgID)
        {
            //Clients.All.receiveNotification("hello");

            if (msgID != null)
            {
                var sender = db.Accounts.Where(a => a.UserName == Context.User.Identity.Name).FirstOrDefault();
                var msgToReply = db.Zinojumi.Find(msgID);
                var user = uList.Where(o => o.UserName == msgToReply.msgFrom.UserName).FirstOrDefault();
                if (user != null)
                {
                    var newMsgCount = db.Zinojumi.Where(a => a.msgTo.ProfileID == msgToReply.msgFrom.ProfileID && !a.isRead).Count();
                    string message = "Jums ir jauna ziņa no " + sender.Vards + " " + sender.Uzvards;
                    Clients.Client(user.ConnectionID).receiveNotification(message, newMsgCount + 1);
                    //Clients.All.receiveNotification(message);
                }
            }
        }
        public void MessageUpdate()
        {
            var user = db.Accounts.Where(a => a.UserName == Context.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var newMessages = db.Zinojumi.Where(a => a.msgTo.ProfileID == user.ProfileID && !a.isRead).Count();
                if (newMessages > 0)
                {
                    var client = uList.Where(o => o.UserName == user.UserName).FirstOrDefault();
                    Clients.Client(client.ConnectionID).UpdateMessageCount(newMessages);
                }
            }
        }
        }
    }