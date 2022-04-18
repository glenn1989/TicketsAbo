using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int? WedstrijdId { get; set; }
        public int? PlaatsId { get; set; }
        public int? AankoopId { get; set; }

        public virtual Aankopen? Aankoop { get; set; }
        public virtual Plaat? Plaats { get; set; }
        public virtual Wedstrijd? Wedstrijd { get; set; }
    }
}
