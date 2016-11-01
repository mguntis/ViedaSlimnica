using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.ViewModel
{
    public class PacientsEditViewModel
    {
        public Pacients Patient { get; set; }
        public IEnumerable<SelectListItem> RoomsFromWhichToSelect { get; set; }
        [DisplayName("Palātas")]
        [Required(ErrorMessage = "*Lauks 'Palātas' nedrīkst būt tukšs")]
        public int SelectedRoomId { get; set; }
    }
}