using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Stadion
    {
        public Stadion()
        {
            Clubs = new HashSet<Club>();
            VakStadions = new HashSet<VakStadion>();
        }

        public int StadionId { get; set; }
        public string? StadionNaam { get; set; }
        public int? StadionCapaciteit { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<VakStadion> VakStadions { get; set; }
    }
}
