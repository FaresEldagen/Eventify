using Eventify.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Models.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public VenueTypeEnum VenueType { get; set; }
        public string? Address { get; set; }
        public string? ZIP { get; set; }
        public CountryEnum Country { get; set; }
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
        public string? ProofOfOwnership { get; set; }

        public VenueVerification? VenueVerification { get; set; }


        // Navigation Property
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
        public List<VenuePhoto> VenuePhotos { get; set; } = new List<VenuePhoto>();
    }
}
