using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.Services
{
    public interface IVenueService : IGenericService<Venue>
    {
        public List<Venue> GetByFilter_Search(
             SortByEnum? sortBy,
             SortingTypeEnum sortingType,
             int pageNumber,
             string? title,
             VenueTypeEnum? venueType,
             int? maxprice,
             CountryEnum? country,
             bool? airConditioning,
             bool? catering,
             bool? wifi,
             bool? parking,
             bool? barService,
             bool? restrooms,
             bool? audioVisual,
             out int totalVenues);

        public List<Venue> GetPendingVenues();

    }

}
