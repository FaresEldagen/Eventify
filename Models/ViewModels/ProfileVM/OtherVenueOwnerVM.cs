using Eventify.ViewModels.VenueVM;

namespace Eventify.ViewModels.ProfileVM
{
    public class OtherVenueOwnerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public string? Gender { get; set; }
        public string? BIO { get; set; }
        public string? Country { get; set; }
        public string JoinedDate { get; set; }
        public int VenueCount { get; set; }
        public decimal WithdrawableEarnings { get; set; }
        public List<VenueCardVM> Venues { get; set; } = new List<VenueCardVM>();
    }
}
