using Eventify.Models.Enums;

namespace Eventify.Models.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string? EventTitle { get; set; }
        public string? Description { get; set; } 
        public bool IsPrivate { get; set; } = false;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Features { get; set; }
        public EventCategoryEnum Category { get; set; }
        public EventStatusEnum Status { get; set; } = EventStatusEnum.Pending;
        public decimal TicketPrice { get; set; }
        public string? Address { get; set; }


        // Navigation Property
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }

        public Payment Payment { get; set; }

        public List<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();
    }
}
