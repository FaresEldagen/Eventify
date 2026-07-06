using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueDetailsOwnerVM : VenueDetailsVM
    {
        public List<Event>? PendingEvents { get; set; }
        public VenueVerification? VenueVerification { get; set; }
    }
}
