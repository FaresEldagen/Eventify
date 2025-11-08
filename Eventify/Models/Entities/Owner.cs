using Eventify.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Models.Entities
{
    public class Owner
    {
        public int OwnerId { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        public string? Photo { get; set; }
        public GenderEnum Gander { get; set; }
        [MaxLength(30)]
        public string? CellPhone { get; set; }
        public string? BIO { get; set; }
        [Required]
        public string Country { get; set; }
        public int VenueCount { get; set; }
        public decimal WithdrawableEarnings { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? FrontIDPhoto { get; set; }
        public string? BackIDPhoto { get; set; }
        public string ArabicFullName { get; set; }
        public string ArabicAddress { get; set; }

        //Navigation Property
        public List<Venue> Venues { get; set; } = new List<Venue>();

    }
}
