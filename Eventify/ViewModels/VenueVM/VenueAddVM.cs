using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Validators;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels.VenueVM
{
    public class VenueAddVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Venue Title is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Venue Type is Required")]
        public VenueTypeEnum VenueType { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        public CountryEnum Country { get; set; }

        [Required(ErrorMessage = "ZIP is Required")]
        public string? ZIP { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Capacity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Price Per Hour is Required")]
        [Range(100, 5000, ErrorMessage = "Price per hour must be greater than 100 and lower than or equal 5000")]
        public int PricePerHour { get; set; }

        public string? SpecialFeatures { get; set; }
        [Required]
        public bool AirConditioningAvailable { get; set; }
        [Required]
        public bool CateringAvailable { get; set; }
        [Required]
        public bool WifiAvailable { get; set; }
        [Required]
        public bool ParkingAvailable { get; set; }
        [Required]
        public bool BarServiceAvailable { get; set; }
        [Required]
        public bool RestroomsAvailable { get; set; }
        [Required]
        public bool AudioVisualEquipment { get; set; }



        [Required(ErrorMessage = $"You must upload the proof of ownership file.")]
        public IFormFile ProofOfOwnershipFile { get; set; }
        public string? ProofOfOwnership { get; set; }


        [MinPhotos(1)]
        public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();
        public List<VenuePhoto> venuePhotos { get; set; } = new List<VenuePhoto>();

    }
}
