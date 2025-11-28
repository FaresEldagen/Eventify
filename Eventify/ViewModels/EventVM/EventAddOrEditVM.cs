using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventAddOrEditVM
    {
        public string? EventTitle { get; set; }
        public EventCategoryEnum Category { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Features { get; set; }
        public bool IsPrivate { get; set; } = false;
        public List<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();

    }
}
