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
        public EventStatusEnum Status { get; set; }

        public decimal TicketPrice { get; set; }   // I Added it, it is not in Mappping so Review it

        // Foreign-Keys
        public int VenueId { get; set; }
        public int OrganizerId { get; set; }

    }
}
