using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ViedaSlimnicaProject.Models
{
    public partial class Palata
    {
        [Key]
        [DisplayName("Palātas Nr.")]
        public int PalatasID { get; set; }
        [DisplayName("Nodaļa")]
        public string Nodala { get; set; }//Nosakam ka kopā mums ir pieci stāvi lai nevar ievadit nepareizus skaitļus
        [DisplayName("Stāvs")]
        public int Stavs { get; set; }//Palatas ietilpiba nevar but lielaka par 4
        [DisplayName("Palātas Ietilpība")]
        public int PalatasIetilpiba { get; set; }
        [DisplayName("Gultas Nr.")]
        public int GultasNr { get; set; }
        public virtual IList<Pacients> Pacienti { get; set; }
        
    }
}