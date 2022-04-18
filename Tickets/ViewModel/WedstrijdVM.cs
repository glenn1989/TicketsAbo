using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tickets.ViewModel
{
    public class WedstrijdVM
    {
        public string? Thuisploeg { get; set; }
        public string? Uitploeg { get; set; }

        public int? ThuisploegId { get; set; }

        public int? VakId { get; set; }

        [Display(Name = "Selecteer thuisploeg")]
        [Required(ErrorMessage = "Verplicht")]
        public IEnumerable<SelectListItem>? Club { get; set; }

        public List<WedstrijdlistVM> wedstrijdlistVMs { get; set; }




    }
}
