using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ViedaSlimnicaProject.Models
{
    public class Palata
    {
        [Key]
        public int PalatasID { get; set; }
        [StringLength(50)]
        [DisplayName("Nodaļa")]
        public string Nodala { get; set; }
        [Range(1, 5)]//Nosakam ka kopā mums ir pieci stāvi lai nevar ievadit nepareizus skaitļus
        [DisplayName("Stāvs")]
        public Nullable<decimal> Stavs { get; set; }
        [Range(1, 4)]//Palatas ietilpiba nevar but lielaka par 4
        [DisplayName("Palātas Ietilpība")]
        public Nullable<decimal> PalatasIetilpiba { get; set; }
        [Range(1, 4)]
        [DisplayName("Gultas Nr.")]
        public Nullable<decimal> GultasNr { get; set; }
        
        public int[] PacientaID { get; set; }

        // const
        /*
        public Palata(int palID, string nod, int sta, int palaIet, Nullable<decimal> gulNr, int[] pacID)
        {
            PalatasID = palID;
            Nodala = nod;
            Stavs = sta;
            GultasNr = gulNr;
            PalatasIetilpiba = palaIet;
            PacientaID = pacID;
        }*/
    }
}