using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViedaSlimnicaProject.Models
{
    public class Profils
    {
            [Key]
            public int ProfileID { get; set; }
            [DisplayName("Lietotājvārds")]
            public string UserName { get; set; }
            [DisplayName("Parole")]
            public string Password { get; set; }
            public string RoleStart { get; set; }
            public virtual Pacients Patient { get; set; }
            public string Vards { get; set; }
            public string Uzvards { get; set; } 
            public bool ToReset { get; set; }
            public bool AccountBlocked { get; set; }
            //public int Attempts { get; set; }
    }

}