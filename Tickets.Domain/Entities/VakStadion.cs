using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class VakStadion
    {
        public VakStadion()
        {
            Plaats = new HashSet<Plaat>();
        }

        public int VakId { get; set; }
        public int Capaciteit { get; set; }
        public int StadionId { get; set; }
        public decimal Prijs { get; set; }

        public virtual Stadion Stadion { get; set; } = null!;
        public virtual Vak Vak { get; set; } = null!;
        public virtual ICollection<Plaat> Plaats { get; set; }
    }
}
