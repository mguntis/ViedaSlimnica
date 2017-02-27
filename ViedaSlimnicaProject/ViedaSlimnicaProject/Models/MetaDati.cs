using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ViedaSlimnicaProject.Models
{
    public partial class PalataMetaData
    {

        [Required(ErrorMessage = "*Lauks 'Nodaļa' nedrīkst būt tukšs")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "*Nodaļas nosaukumam jābūt minimums 3 rakstzīmēm un maksimums 30")]
        public string Nodala;

        [Required(ErrorMessage = "*Lauks 'Stāvs' nedrīkst būt tukšs")]
        [RegularExpression(@"^\d+$", ErrorMessage = "*Var ievadīt tikai veselus skaitļus")]
        [Range(1, 5, ErrorMessage = "*Stāvi ir no 1 līz 5")]
        public int Stavs;

        [Required(ErrorMessage = "*Lauks 'Palātas ietilpība' nedrīkst būt tukšs")]
        [RegularExpression(@"^\d+$", ErrorMessage = "*Var ievadīt tikai veselus skaitļus")]
        [Range(1, 4,ErrorMessage = "*Palātas ietilpība ir no 1 līdz 4")]
        public int PalatasIetilpiba;
        

    }

   public class PacientsMetaDati
    {
        // [RegularExpression(@"^[a-zA-Z\u0410-\u042F\u0430-\u044F\u0401\u0451\u0101\u0100\u010c\u010d\u0112\u0113\u011E\u011F\u012A\u012B\u0136\u0137\u013b\u013C\u0145\u0146\u0160\u0161\u016A\u016B\u017D\u017E]+$", ErrorMessage = "*Ievadīts neatbilstošs vārds")]
        [RegularExpression(@"[^0-9()[\]{}*&^%$#@!]+", ErrorMessage = "*Ievadīts neatbilstošs vārds")]
        [StringLength(30, ErrorMessage = "*Vārdam jābūt minimums 3 rakstzīmēm un maksimums 30")]
        [Required(ErrorMessage = "*Lauks 'Vārds' nedrīkst būt tukšs")]
        public string Vards;

        //[RegularExpression(@"^[a-zA-Z\u0410-\u042F\u0430-\u044F\u0401\u0451\u0101\u0100\u010c\u010d\u0112\u0113\u011E\u011F\u012A\u012B\u0136\u0137\u013b\u013C\u0145\u0146\u0160\u0161\u016A\u016B\u017D\u017E]+$", ErrorMessage = "*Ievadīts neatbilstošs uzvārds")]
        [RegularExpression(@"[^0-9()[\]{}*&^%$#@!]+", ErrorMessage = "*Ievadīts neatbilstošs uzvārds")]
        [StringLength(30, ErrorMessage = "*Uzvārdam jābūt minimums 3 rakstzīmēm un maksimums 30")]
        [Required(ErrorMessage = "*Lauks 'Uzvārds' nedrīkst būt tukšs")]
        public string Uzvards;

        [RegularExpression(@"^\d{6}-\d{5}$", ErrorMessage = "Lūdzu ievadiet pareizu personas kodu")]
        [Required(ErrorMessage = "*Lauks 'Personas kods' nedrīkst būt tukšs")]
        [StringLength(12, MinimumLength = 12)]
        public string PersKods;

        [Required(ErrorMessage = "*Lauks E-pasts' nedrīkst būt tukšs")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Ievadīts nekorekts e-pasts")]
        [StringLength(50, MinimumLength = 3)]
        public string Epasts { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "*Lūdzu ievadiet pareizu telefona numuru")]
        [Required(ErrorMessage = "*Lauks 'Telefona numurs' nedrīkst būt tukšs")]
        [StringLength(8, MinimumLength = 8)]
        public string TNumurs { get; set; }

        /*[StringLength(160)]
        [Display(Name = "Simptomi")]
        public string Simptomi;*/
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "*Lauks 'Ierašanās datums' nedrīkst būt tukšs")]
        public Nullable<System.DateTime> IerasanasDatums;
    }

}