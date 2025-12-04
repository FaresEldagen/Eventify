using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueEditVM : VenueAddVM
    {
        public List<string> DeletedPhotos { get; set; } = new List<string>();
    }
}
