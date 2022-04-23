namespace Tickets.ViewModel
{
    public class OverviewListVM
    {
        public List<OverviewVM>? OverviewList { get; set; }
    }
    
    public class OverviewVM
    {
        public string? Thuisploeg { get; set; }
        public string? Uitploeg { get; set; }
        public string Abonnementsploeg { get; set; }
        public int? Vaknummer { get; set; }
        public int? Pleknummer { get; set; }
        public int? TicketID { get; set; }

        public bool isAbo { get; set; }

        

    }
}
