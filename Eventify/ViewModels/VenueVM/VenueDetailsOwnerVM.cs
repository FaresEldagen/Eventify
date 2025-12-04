using Eventify.Models.Entities;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueDetailsOwnerVM : VenueDetailsVM
    {
        public List<Event>? PendingEvents { get; set; }
    }
}
