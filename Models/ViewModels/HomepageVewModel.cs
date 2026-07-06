using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;

namespace Eventify.ViewModels
{
    public class HomepageVewModel
    {
        public List<EventCardVM> UpcomingEvents { get; set; }
        public List<VenueCardVM> TopVenues { get; set; }
    }
}
