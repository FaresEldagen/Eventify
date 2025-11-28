namespace Eventify.ViewModels.VenueVM
{
    public class VenueCardVM
    {
        public string? VenueName { get; set; }
        public string? VenueType { get; set; }
        public string? City { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
