using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Seeding_Data
{
    public class VenuesSeedData : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.HasData(
                new Venue
                {
                    Id = 1,
                    Name = "Cairo Grand Hall",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "Nasr City",
                    Country = CountryEnum.Egypt,
                    ZIP = "11371",
                    Description = "Large premium indoor venue.",
                    Capacity = 500,
                    PricePerHour = 2500,
                    SpecialFeatures = "Stage, LED Screens",
                    AirConditioningAvailable = true,
                    CateringAvailable = true,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = true,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 1
                },
                new Venue
                {
                    Id = 2,
                    Name = "Dubai Outdoor Arena",
                    VenueType = VenueTypeEnum.Outdoor,
                    Address = "Marina Walk",
                    Country = CountryEnum.UnitedArabEmirates,
                    ZIP = "00000",
                    Description = "Open-air event arena with sea view.",
                    Capacity = 300,
                    PricePerHour = 1800,
                    SpecialFeatures = "Sea View",
                    AirConditioningAvailable = false,
                    CateringAvailable = true,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = true,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 2
                },
                new Venue
                {
                    Id = 3,
                    Name = "Riyadh Event Center",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "Olaya Street",
                    Country = CountryEnum.SaudiArabia,
                    ZIP = "11564",
                    Description = "Modern multi-purpose venue.",
                    Capacity = 700,
                    PricePerHour = 3000,
                    SpecialFeatures = "VIP Rooms",
                    AirConditioningAvailable = true,
                    CateringAvailable = false,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = true,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 3
                },
                new Venue
                {
                    Id = 4,
                    Name = "Alexandria Seaside Hall",
                    VenueType = VenueTypeEnum.Outdoor,
                    Address = "Corniche Road",
                    Country = CountryEnum.Egypt,
                    ZIP = "21500",
                    Description = "Outdoor venue with stunning sea view.",
                    Capacity = 250,
                    PricePerHour = 1500,
                    SpecialFeatures = "Sea Breeze, Open Stage",
                    AirConditioningAvailable = false,
                    CateringAvailable = true,
                    WifiAvailable = true,
                    ParkingAvailable = false,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 1
                },
                new Venue
                {
                    Id = 5,
                    Name = "Giza Pyramid Arena",
                    VenueType = VenueTypeEnum.Outdoor,
                    Address = "Pyramids Road",
                    Country = CountryEnum.Egypt,
                    ZIP = "12556",
                    Description = "Cultural event venue facing pyramids.",
                    Capacity = 600,
                    PricePerHour = 3500,
                    SpecialFeatures = "Historic View",
                    AirConditioningAvailable = false,
                    CateringAvailable = true,
                    WifiAvailable = false,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 1
                },
                new Venue
                {
                    Id = 6,
                    Name = "Abu Dhabi Royal Hall",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "Khalifa Street",
                    Country = CountryEnum.UnitedArabEmirates,
                    ZIP = "00001",
                    Description = "Luxury indoor venue for premium events.",
                    Capacity = 450,
                    PricePerHour = 4000,
                    SpecialFeatures = "Gold Interior, VIP Lounge",
                    AirConditioningAvailable = true,
                    CateringAvailable = true,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = true,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = true,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 2
                },
                new Venue
                {
                    Id = 7,
                    Name = "Jeddah Beach Stage",
                    VenueType = VenueTypeEnum.Outdoor,
                    Address = "North Corniche",
                    Country = CountryEnum.SaudiArabia,
                    ZIP = "23415",
                    Description = "Outdoor beach concert venue.",
                    Capacity = 800,
                    PricePerHour = 2800,
                    SpecialFeatures = "Beachfront Stage",
                    AirConditioningAvailable = false,
                    CateringAvailable = false,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = true,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 3
                },
                new Venue
                {
                    Id = 8,
                    Name = "Doha Convention Hall",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "West Bay",
                    Country = CountryEnum.Qatar,
                    ZIP = "00022",
                    Description = "Large conference and exhibition hall.",
                    Capacity = 1000,
                    PricePerHour = 5000,
                    SpecialFeatures = "Conference Rooms",
                    AirConditioningAvailable = true,
                    CateringAvailable = false,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = true,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 2
                },
                new Venue
                {
                    Id = 9,
                    Name = "Luxor Cultural Theatre",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "Luxor Temple Road",
                    Country = CountryEnum.Egypt,
                    ZIP = "85958",
                    Description = "Historic indoor cultural venue.",
                    Capacity = 350,
                    PricePerHour = 1700,
                    SpecialFeatures = "Theatre Stage",
                    AirConditioningAvailable = true,
                    CateringAvailable = false,
                    WifiAvailable = false,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 1
                },
                new Venue
                {
                    Id = 10,
                    Name = "Sharjah Art Gallery",
                    VenueType = VenueTypeEnum.Indoor,
                    Address = "Art District",
                    Country = CountryEnum.UnitedArabEmirates,
                    ZIP = "00033",
                    Description = "Gallery space for exhibitions.",
                    Capacity = 120,
                    PricePerHour = 900,
                    SpecialFeatures = "Art Lighting",
                    AirConditioningAvailable = true,
                    CateringAvailable = false,
                    WifiAvailable = true,
                    ParkingAvailable = true,
                    BarServiceAvailable = false,
                    RestroomsAvailable = true,
                    AudioVisualEquipment = false,
                    ProofOfOwnership = "/Images/ownership1.jpg",
                    OwnerId = 2
                }
            );
        }
    }
}