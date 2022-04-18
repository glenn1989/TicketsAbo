using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tickets.ViewModel
{
    public class TicketVM
    {
        public int wedstrijdID { get; set; }
        public string? Thuisploeg { get; set; }
        public string? Uitploeg { get; set; }

        [Range(1,4,ErrorMessage ="Maximum 4 kaarten per persoon.")]

        [Display(Name ="Aantal Tickets")]
        public int aantalTickets { get; set; }

        [Required(ErrorMessage ="Gelieve vak te selecteren")]

        [Display(Name ="Vak Selecteren")]
        public IEnumerable<SelectListItem>? Vak { get; set; }

        public int VakId { get; set; }

        public decimal Prijs { get; set; }

        public int? StadionId { get; set; }

    }
}
