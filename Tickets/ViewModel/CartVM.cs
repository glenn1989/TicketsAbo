using System.ComponentModel.DataAnnotations;

namespace Tickets.ViewModel
{

    public class ShoppingCartVM
    {
        public List<CartVM>? Cart { get; set; }
    }
    public class CartVM
    {
        public string? Thuisploeg { get; set; }
        public int? ThuisploegId { get; set; }
        public int? UitploegId { get; set; }
        public string? Uitploeg { get; set; }
        [Display(Name ="Aantal tickets")]
        public int AantalTickets { get; set; }
        public string? Stadion { get; set; }

        public int? StadionId { get; set; }
        public float? Prijs { get; set; }
        public string? Vak { get; set; }
        public int? VakId { get; set; }
        public System.DateTime Aankoopdatum { get; set; }
        [Display(Name ="Wedstrijd/Abonnement")]
        public int? WedstrijdId { get; set; }

        public bool? IsAbonnement { get; set; }

        public DateTime Datum { get; set; }

    }
}
