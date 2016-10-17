using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoSite.Models
{
    public class Palata
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Parāk garš nodaļas nosaukums")]
        public string Nodala;

        [Range(0, 5)]
        public Nullable<decimal> Stavs;

        [Range(0, 4)]
        public Nullable<decimal> PalatasIetilpiba;

        [Range(0, 4)]
        public Nullable<decimal> GultasNr;

    }

    public class Pacients
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Vārds nedrīkst pārsniegt 50 burtus")]
        public string Vards;

        [StringLength(50)]
        [Required(ErrorMessage = "Uzvārds nedrīkst pārsniegt 50 burtus")]
        public string Uzvards;

        [StringLength(50)]
        [Required(ErrorMessage = "Nepareizi ievadits personas kods")]
        public string PersKods;

        [StringLength(160)]
        [Display(Name = "Simptomi")]
        public string Simptomi;

        [StringLength(50)]
        [Display(Name = "Nepareiz nodaļas nosaukums")]
        public string Nodala;

        [Required(ErrorMessage = "Nepareizs datums")]
        public Nullable<System.DateTime> EnrollmentDate;
    }
}