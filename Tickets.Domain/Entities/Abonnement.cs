using System;
using System.Collections.Generic;

namespace Tickets.Domain.Entities
{
    public partial class Abonnement
    {
        public int Abonnementsnummer { get; set; }
        public int? Stamnummer { get; set; }
        public int? PlaatsId { get; set; }
        public int? AankoopId { get; set; }

        public virtual Aankopen? Aankoop { get; set; }
        public virtual Plaat? Plaats { get; set; }
        public virtual Club? StamnummerNavigation { get; set; }
    }
}
