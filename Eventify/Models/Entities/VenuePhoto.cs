namespace Eventify.Models.Entities
{
    public class VenuePhoto
    {
        public string? PhotoUrl { get; set; }


        // Navigation Property
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
    }
}
