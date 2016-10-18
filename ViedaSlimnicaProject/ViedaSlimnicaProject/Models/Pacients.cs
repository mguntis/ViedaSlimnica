using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ViedaSlimnicaProject.Models
{
    public partial class Pacients
    {
        [Key]
        public int PacientaID { get; set; }
        [StringLength(50)]
        [DisplayName("Vārds")]
        public string Vards { get; set; }
        [StringLength(50)]
        [DisplayName("Uzvārds")]
        public string Uzvards { get; set; }
        [StringLength(12)]
        [DisplayName("Personas kods")]
        public string PersKods { get; set; }
        [StringLength(50)]
        [DisplayName("Simptomi")]
        public string Simptomi { get; set; }
        [StringLength(50)]
        [DisplayName("Nodaļa")]
        public string Nodala { get; set; } //Tas arī ir definēts klassē palātā bet nevar izmantot to pašu? 
        [DisplayName("Pālātas Nr.")]
        public int PalatasID { get; set; }
        [DisplayName("Ierašanās datums")]
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