using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ViedaSlimnicaProject.Models
{
    public partial class Pacients
    {
        [Key]
        public int PacientaID { get; set; }
        [StringLength(50)]
        public string Vards { get; set; }
        [StringLength(50)]
        public string Uzvards { get; set; }
        [StringLength(12)]
        public string PersKods { get; set; }
        [StringLength(50)]
        public string Simptomi { get; set; }
        [StringLength(50)]
        public string Nodala { get; set; } //Tas arī ir definēts klassē palātā bet nevar izmantot to pašu? 
        public int PalatasID { get; set; }
        public DateTime IerasanasDatums { get; set; }

        //Test
       /* public Pacients(int pacID, string vard, string uzvar, string perskod, string simp, string nod, int palID, DateTime ier)
        {
            PacientaID = pacID;
            Vards = vard;
            Uzvards = uzvar;
            PersKods = perskod;
            Simptomi = simp;
            Nodala = nod;
            PalatasID = palID;
            IerasanasDatums = ier;
        }*/
    }
}