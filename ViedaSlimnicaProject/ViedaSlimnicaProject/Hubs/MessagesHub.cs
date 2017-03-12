using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ViedaSlimnicaProject.Hubs
{
    public class MessagesHub : Hub
    {
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
            }
        }
    }
}