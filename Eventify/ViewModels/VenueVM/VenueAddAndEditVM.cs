using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueAddAndEditVM
    {
        public string? Name { get; set; }
        public VenueTypeEnum VenueType { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZIP { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal PricePerDay { get; set; }
        public string? SpecialFeatures { get; set; }
        public bool AirConditioningAvailable { get; set; }
        public bool CateringAvailable { get; set; }
        public bool WifiAvailable { get; set; }
        public bool ParkingAvailable { get; set; }
        public bool BarServiceAvailable { get; set; }
        public bool RestroomsAvailable { get; set; }
        public bool AudioVisualEquipment { get; set; }
        public string? ProofOfOwnership { get; set; }
        public List<VenuePhoto>?venuePhotos { get; set; }

    }
}
