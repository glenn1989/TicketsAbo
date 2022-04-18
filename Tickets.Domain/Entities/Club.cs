using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            Abonnements = new HashSet<Abonnement>();
            WedstrijdThuisploegs = new HashSet<Wedstrijd>();
            WedstrijdUitploegs = new HashSet<Wedstrijd>();
        }

        public int Stamnummer { get; set; }
        public string? Clubnaam { get; set; }
        public int? StadionId { get; set; }

        public virtual Stadion? Stadion { get; set; }
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdThuisploegs { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdUitploegs { get; set; }
    }
}
