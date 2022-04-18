namespace Tickets.ViewModel
{
    public class WedstrijdlistVM
    {
        public int WedstrijdId { get; set; }
        public int? ThuisploegId { get; set; }

        public string? Thuisploeg { get; set; }

        public int? UitploegId { get; set; }

        public string? Uitploeg { get; set; }
        public DateTime? Datum { get; set; }
        public TimeSpan? Uur { get; set; }

    }
}
