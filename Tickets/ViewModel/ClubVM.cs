using System.ComponentModel.DataAnnotations;

namespace Tickets.ViewModel
{
    public class ClubVM
    {
        public int Stamnummer { get; set; }
        public string? Clubnaam { get; set; }
        public string? Stadion { get; set; }

        public int? Capaciteit { get; set; }
    }
}
