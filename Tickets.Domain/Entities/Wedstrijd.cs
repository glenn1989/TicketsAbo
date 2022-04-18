using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Wedstrijd
    {
        public Wedstrijd()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int WedstrijdId { get; set; }
        public int? ThuisploegId { get; set; }
        public int? UitploegId { get; set; }
        public DateTime? Datum { get; set; }
        public TimeSpan? Uur { get; set; }
        public int? CompetitieId { get; set; }

        public virtual Competitie? Competitie { get; set; }
        public virtual Club? Thuisploeg { get; set; }
        public virtual Club? Uitploeg { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
