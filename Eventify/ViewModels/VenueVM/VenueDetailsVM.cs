using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueDetailsVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public VenueTypeEnum VenueType { get; set; }
        public string? Address { get; set; }
        public CountryEnum Country { get; set; }
        public string? ZIP { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public int PricePerHour { get; set; }
        public string? SpecialFeatures { get; set; }
        public bool AirConditioningAvailable { get; set; }
        public bool CateringAvailable { get; set; }
        public bool WifiAvailable { get; set; }
        public bool ParkingAvailable { get; set; }
        public bool BarServiceAvailable { get; set; }
        public bool RestroomsAvailable { get; set; }
        public bool AudioVisualEquipment { get; set; }
        public List<VenuePhoto>? venuePhotos { get; set; }
        public List<DateRange>? DateRange { get; set; }
        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }
    }
}
