namespace Tickets.ViewModel
{
    public class OrderVM
    {
        public int AankoopId { get; set; }
        public string UserID { get; set; }
        public DateTime Aankoopdatum { get; set; }
    }

    public class PlaatsVM
    {
        public int VakID { get; set; }
        public int Plaatsnummer { get; set; }
        public int StadionID { get; set; }

    }

    public class TicketOrderVM
    {
        public int WedstrijdID { get; set; }
        public int PlaatsID { get; set; }
        public int AankoopID { get; set; }

    }
}
