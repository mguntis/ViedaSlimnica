using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViedaSlimnicaProject.Models
{
    public class Zinojumi
    {
        [Key]
        public int zinojumaID { get; set; }
        [DisplayName("Ziņojuma Teksts")]
        public string msg { get; set; }
        public virtual Profils profils { get; set; }  // kas izsūtīja šo ziņojumu
        public DateTime date { get; set; } // datums 
        public string dateString { get; set; } // datums noformatesanai
        public Pacients msgTo { get; set; }
    }
}