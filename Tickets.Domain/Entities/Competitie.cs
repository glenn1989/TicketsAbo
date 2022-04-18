using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Competitie
    {
        public Competitie()
        {
            Wedstrijds = new HashSet<Wedstrijd>();
        }

        public int CompetitieId { get; set; }
        public string? Competitienaam { get; set; }
        public DateTime? Begindatum { get; set; }
        public DateTime? Einddatum { get; set; }

        public virtual ICollection<Wedstrijd> Wedstrijds { get; set; }
    }
}
