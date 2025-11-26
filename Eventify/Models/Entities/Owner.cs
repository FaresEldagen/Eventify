using Eventify.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Eventify.Models.Entities
{
    public class Owner : ApplicationUser
    {
        public int VenueCount { get; set; }
        public decimal WithdrawableEarnings { get; set; }

        //Navigation Property
        public List<Venue> Venues { get; set; } = new List<Venue>();

    }
}
