﻿using System;
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

        //public virtual IList<Pacients> msgTo { get; set; } // pagaidām ziņojumi visiem, bet ja gribēsim izsūtīt dažiem pacientiem.
    }
}