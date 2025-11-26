using Eventify.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Eventify.Models.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? Photo { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? BIO { get; set; }
        public CountryEnum? Country { get; set; }
        public string? FrontIdPhoto { get; set; }
        public string? BackIdPhoto { get; set; }
        public string? ArabicAddress { get; set; }
        public string? ArabicFullName { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? NationalIDNumber { get; set; }



    }
}
