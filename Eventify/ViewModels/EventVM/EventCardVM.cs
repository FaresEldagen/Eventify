using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventCardVM
    {

        public string? EventTitle { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? Address { get; set; }
        public decimal TicketPrice { get; set; }
        public EventCategoryEnum Category { get; set; }
        public string? EventPhoto { get; set; }


    }
}
