using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventCardVM
    {
        public int Id { get; set; }
        public string EventTitle { get; set; }
        public string StartDateTime { get; set; }
        public string Address { get; set; }
        public decimal TicketPrice { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string EventPhoto { get; set; }
    }
}
