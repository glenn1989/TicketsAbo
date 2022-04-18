using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            VakStadions = new HashSet<VakStadion>();
        }

        public int VakId { get; set; }
        public string? VakNaam { get; set; }

        public virtual ICollection<VakStadion> VakStadions { get; set; }
    }
}
