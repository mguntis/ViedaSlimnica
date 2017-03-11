using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.ViewModel
{
    public class ZinojumiView
    {
        public IEnumerable<Zinojumi> Received { get; set; }
        public IEnumerable<Zinojumi> Sent { get; set; }
        public IEnumerable<Profils> AvailableRecipents { get; set; }
    }
}