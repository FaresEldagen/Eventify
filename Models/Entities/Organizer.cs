using Eventify.Models.Enums;

namespace Eventify.Models.Entities
{
    public class Organizer : ApplicationUser
    {
        public int ExperienceYear { get; set; }
        public int PastEventCount { get; set; }
        public string? Specialization { get; set; }
        

        // Nagigation-properties
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
