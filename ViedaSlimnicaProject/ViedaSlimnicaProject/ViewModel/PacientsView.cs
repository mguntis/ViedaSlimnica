using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.ViewModel
{
    public class PacientsView
    {
        public int id { get; set; }
        public Pacients Pacients { get; set; }
        public IEnumerable<Zinojumi> Msg { get; set; }
    }
}