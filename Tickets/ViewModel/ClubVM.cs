using System.ComponentModel.DataAnnotations;

namespace Tickets.ViewModel
{

    public class ClubListVM
    {
        public List<ClubVM> clublist { get; set; }
    }
    public class ClubVM
    {
        public int Stamnummer { get; set; }
        public string? Clubnaam { get; set; }
        public string? Stadion { get; set; }
        public int? Capaciteit { get; set; }

        public int? Abonnees { get; set; }
    }
}
