using Eventify.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels.ProfileVM
{
    public class EditProfileVM
    {
        public int? id { get; set; }
        public GenderEnum? Gender { get; set; }
        public CountryEnum? Country { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? BIO { get; set; }
        public int ExperienceYear { get; set; } = 0;

        [MaxLength(20)]
        public string? Specialization { get; set; }

        [MaxLength(150)]
        public string? ArabicAddress { get; set; }

        [MaxLength(150)]
        public string? ArabicFullName { get; set; }

        [MaxLength(50)]
        public string? NationalIDNumber { get; set; }

        public string? Photo { get; set; }
        public bool RemovePhoto { get; set; } 
        public IFormFile? PhotoFile { get; set; }

        public string? FrontIdPhoto { get; set; }
        public bool RemoveFrontIdPhoto { get; set; } 
        public IFormFile? FrontIdFile { get; set; }

        public string? BackIdPhoto { get; set; }
        public bool RemoveBackIdPhoto { get; set; } 
        public IFormFile? BackIdFile { get; set; }

        public AccountStatus? AccountStatus { get; set; }

    }
}
