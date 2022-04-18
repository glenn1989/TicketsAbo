using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Aankopen
    {
        public Aankopen()
        {
            Abonnements = new HashSet<Abonnement>();
            Tickets = new HashSet<Ticket>();
        }

        public int AankoopId { get; set; }
        public string? ClientId { get; set; }
        public DateTime? Aankoopdatum { get; set; }

        public virtual AspNetUser? Client { get; set; }
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
