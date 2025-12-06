using Eventify.Models.Enums;

namespace Eventify.ViewModels.ProfileVM
{
    public class EditProfileVM
    {
        public GenderEnum? Gender { get; set; }
        public CountryEnum? Country { get; set; }
        public string? Phone { get; set; }
        public string? BIO { get; set; }
        public int ExperienceYear { get; set; } = 0;
        public string? Specialization { get; set; }
        public string? ArabicAddress { get; set; }
        public string? ArabicFullName { get; set; }
        public string? NationalIDNumber { get; set; }

        public string? Photo { get; set; }
        public bool RemovePhoto { get; set; } 
        public IFormFile? PhotoFile { get; set; }

        public string? FrontIdPhoto { get; set; }
        public bool RemoveFrontIdPhoto { get; set; } 
        public IFormFile? FrontIdFile { get; set; }

        public string? BackIdPhoto { get; set; }
        public bool RemoveBacktIdPhoto { get; set; } 
        public IFormFile? BackIdFile { get; set; }

        public AccountStatus? AccountStatus { get; set; }

    }
}
