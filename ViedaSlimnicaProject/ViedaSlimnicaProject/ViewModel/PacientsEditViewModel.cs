using System.Collections.Generic;
using System.Web.Mvc;
using ViedaSlimnicaProject.Models;

namespace ViedaSlimnicaProject.ViewModel
{
    public class PacientsEditViewModel
    {
        public Pacients Patient { get; set; }
        public IEnumerable<SelectListItem> RoomsFromWhichToSelect { get; set; }
        public int SelectedRoomId { get; set; }
    }
}