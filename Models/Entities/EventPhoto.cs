namespace Eventify.Models.Entities
{
    public class EventPhoto
    {
        public string? PhotoUrl { get; set; }


        // Navigation Property
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
