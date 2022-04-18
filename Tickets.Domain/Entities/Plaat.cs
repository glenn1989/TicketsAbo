using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Plaat
    {
        public Plaat()
        {
            Abonnements = new HashSet<Abonnement>();
            Tickets = new HashSet<Ticket>();
        }

        public int PlaatsId { get; set; }
        public int? Plaatsnummer { get; set; }
        public int? StadionId { get; set; }
        public int? VakId { get; set; }

        public virtual VakStadion? VakStadion { get; set; }
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
