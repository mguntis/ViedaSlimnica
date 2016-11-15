using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViedaSlimnicaProject.Models
{
    public class Profils
    {
            [Key]
            public int ProfileID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public UserRoles URole { get; set; }
            public virtual IList<Pacients> Pacienti { get; set; }
    }

}