using Eventify.Models.Enums;

namespace Eventify.Models.Entities
{
    public class Organizier
    {
        public int OrganizierId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Photo { get; set; }
        public GenderEnum Gender { get; set; }
        public string? BIO { get; set; }
        public string? Country { get; set; }
        public int ExperienceYear { get; set; }
        public int PastEventCount { get; set; }
        public string? Specialization { get; set; }
        public string? Email { get; set; }
        public string? FrontIDPhoto { get; set; }
        public string? BackIDPhoto { get; set; }
        public string? NationalIDNumber { get; set; }
        public string? ArabicAddress { get; set; }
        public string? ArabicFullName { get; set; }
        public string? CellPhone { get; set; }
        public DateTime JoinedDate { get; set; }


    }
}
