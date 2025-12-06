namespace Eventify.ViewModels.VenueVM
{
    public class VenueCardVM
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string VenueType { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }

    }
}