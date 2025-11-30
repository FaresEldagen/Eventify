using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventAddOrEditVM
    {
        public int EventId { get; set; }
        public string? EventTitle { get; set; }
        public EventCategoryEnum Category { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime EndDateTime { get; set; } = DateTime.Now;
        public decimal TicketPrice { get; set; }
        public string? Features { get; set; }
        public bool IsPrivate { get; set; } = false;
        public int VenueId { get; set; }
        public List<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();
        public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();

        public List<string> DeletedPhotos { get; set; } = new List<string>();


    }
}
