using Eventify.Models.Enums;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueBrowseViewModel
    {
        public int? sortBy { get; set; } = null;
        public int sortingType { get; set; } = 1;
        public int pageNumber { get; set; }
        public string? title { get; set; }
        public int? venueType { get; set; }
        public int? maxprice { get; set; }
        public CountryEnum? country { get; set; }
        public bool airConditioning { get; set; }
        public bool catering { get; set; }
        public bool wifi { get; set; }
        public bool parking { get; set; }
        public bool barService { get; set; }
        public bool restrooms { get; set; }
        public bool audioVisual { get; set; }

        public List<VenueCardVM> venueCards { get; set; } = new List<VenueCardVM>();

        public int TotalPages { get; set; }

        public int TotalVenues { get; set; }


        public VenueBrowseViewModel()
        {
            sortBy = null;
            sortingType = 1;
            pageNumber = 1;
            title = null;
            venueType = null;
            maxprice = 5000;
            country = null;
            airConditioning = false;
            catering = false;
            wifi = false;
            parking = false;
            barService = false;
            restrooms = false;
            audioVisual = false;
            venueCards = new List<VenueCardVM>();
            TotalPages = 1;
            TotalVenues = 9;
        }
    }
}
