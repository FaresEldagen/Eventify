using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventDetailsVM
    {
        public string? EventTitle { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public EventStatusEnum Status { get; set; } 
        public decimal TicketPrice { get; set; }
        public string? Features { get; set; }
        public string? Address { get; set; }
        public string OrganizerName { get; set; }
        public int OrganizerId { get; set; }
        public int Capacity { get; set; }
        public List<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();
        public EventCategoryEnum Category { get; set; }

    }
}
