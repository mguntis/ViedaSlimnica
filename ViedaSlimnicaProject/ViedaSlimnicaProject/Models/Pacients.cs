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
        [DisplayName("Vārds")]
        public string Vards { get; set; }
        [DisplayName("Uzvārds")]
        public string Uzvards { get; set; }
        [DisplayName("Personas kods")]
        public string PersKods { get; set; }
        [DisplayName("E-Pasts")]
        public string Epasts { get; set; }
        [DisplayName("Telefona numurs")]
        public string TNumurs { get; set; }
        /*[DisplayName("Simptomi")]
        public string Simptomi { get; set; }*/
        //[DisplayName("Nodaļa")]
       // public string Nodala { get; set; } //Tas arī ir definēts klassē palātā bet nevar izmantot to pašu? 
        [DisplayName("Pālātas Nr.")]
        public virtual Palata Palata { get; set; }
        [DisplayName("Ierašanās datums")]
        public DateTime IerasanasDatums { get; set; }
        public virtual Profils ProfileAcc { get; set; }
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