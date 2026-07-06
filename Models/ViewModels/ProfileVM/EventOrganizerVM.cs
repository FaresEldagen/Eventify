using Eventify.Models.Enums;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;

namespace Eventify.ViewModels.ProfileVM
{
    public class EventOrganizerVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? Gender { get; set; }
        public string? BIO { get; set; }
        public string? Country { get; set; }
        public string? JoinedDate { get; set; }
        public int? ExperienceYear { get; set; }
        public string? Specialization { get; set; }
        public int EventsCount { get; set; }
        public List<EventCardVM> Events { get; set; } = new List<EventCardVM>();
        public string? Verfication { get; set; }
    }
}
