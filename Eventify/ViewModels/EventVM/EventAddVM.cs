using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Validators;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels.EventVM
{
    public class EventAddVM
    {
        public int EventId { get; set; }


        [Required(ErrorMessage = "Event Title is Required")]
        public string EventTitle { get; set; }


        [Required(ErrorMessage = "Event Category is Required")]
        public EventCategoryEnum Category { get; set; }


        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Start Date Time is Required")]
        public DateTime StartDateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End Date Time is Required")]
        [EndDateAfterStart("StartDateTime")]
        public DateTime EndDateTime { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Ticket Price is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Ticket Price must be greater than 0")]
        public decimal TicketPrice { get; set; }


        public string? Features { get; set; }


        [Required]
        public bool IsPrivate { get; set; } = false;


        [Required]
        public int VenueId { get; set; }

        public List<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();

        [MinPhotos(1)]
        public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();
    }
}
