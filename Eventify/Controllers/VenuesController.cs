using Eventify.Managers;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication2.Controllers
{

    public class VenuesController : Controller
    {
        private readonly IVenueService _venueManager;
        public VenuesController(IVenueService venueManager)
        {
            _venueManager = venueManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var venues = _venueManager.GetByFilter_Search
                (
                    sortBy: null,
                    sortingType: SortingTypeEnum.Ascending,
                    pageNumber: 1,
                    title: null,
                    venueType: null,
                    maxprice: null,
                    country: null,
                    airConditioning: null,
                    catering: null,
                    wifi: null,
                    parking: null,
                    barService: null,
                    restrooms: null,
                    audioVisual: null,
                    out int totalVenues
                );

            var venueCards = venues.Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    Type = v.VenueType.ToString(),
                    Address = v.Address!,
                    Capacity = v.Capacity,
                    PricePerHour = v.PricePerHour,
                    Photo = v.VenuePhotos.FirstOrDefault()!.PhotoUrl!
                }).ToList();

            var totalFilterVenues = totalVenues;

            //ViewBag.VenueCards = venueCards;
            var venueViewModel = new VenueBrowseViewModel();
            venueViewModel.venueCards = venueCards;
            venueViewModel.TotalPages = (int)Math.Ceiling(totalVenues / 9.0m);
            return View("Index", venueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(VenueBrowseViewModel venueBrowseViewModel)
        {
            var venues = _venueManager.GetByFilter_Search
                (
                    sortBy: (venueBrowseViewModel.sortBy == null) ? null : (SortByEnum)venueBrowseViewModel.sortBy,
                    sortingType: (venueBrowseViewModel.sortingType == null) ? SortingTypeEnum.Ascending : (SortingTypeEnum)venueBrowseViewModel.sortingType,
                    pageNumber: venueBrowseViewModel.pageNumber == 0 ? 1 : venueBrowseViewModel.pageNumber,
                    title: venueBrowseViewModel.title,
                    venueType: (venueBrowseViewModel.venueType == null) ? null : (VenueTypeEnum)venueBrowseViewModel.venueType,
                    maxprice: venueBrowseViewModel.maxprice,
                    country: venueBrowseViewModel.country,
                    airConditioning: venueBrowseViewModel.airConditioning,
                    catering: venueBrowseViewModel.catering,
                    wifi: venueBrowseViewModel.wifi,
                    parking: venueBrowseViewModel.parking,
                    barService: venueBrowseViewModel.barService,
                    restrooms: venueBrowseViewModel.restrooms,
                    audioVisual: venueBrowseViewModel.audioVisual,
                    out int totalVenues
                );

            var venueCards = venues.Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    Type = v.VenueType.ToString(),
                    Address = v.Address!,
                    Capacity = v.Capacity,
                    PricePerHour = v.PricePerHour,
                    Photo = v.VenuePhotos.FirstOrDefault()!.PhotoUrl!
                }).ToList();

            var totalFilterVenues = totalVenues;

            venueBrowseViewModel.venueCards = venueCards;
            venueBrowseViewModel.TotalPages = (int)Math.Ceiling(totalFilterVenues / 9.0m);
            //return View(venueBrowseViewModel);
            return PartialView("_SearchVenues", venueBrowseViewModel);
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var venue = _venueManager.GetByIdWithIncludes(id);

            if (venue == null)
            {
                return NotFound();
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out int currentUserId);

            var bookedDates = venue.Events.Where(e => e.Status == Eventify.Models.Enums.EventStatusEnum.Approved || e.Status == Eventify.Models.Enums.EventStatusEnum.Paid)
                .Select(e => new DateRange
                {
                    StartDate = e.StartDateTime,
                    EndDate = e.EndDateTime
                }).ToList();

            var pendingEventsList = venue.Events
                .Where(e => e.Status == Eventify.Models.Enums.EventStatusEnum.Pending)
                .ToList();

            if (User.IsInRole("Owner") && venue.OwnerId == currentUserId)
            {
                var venueDetailsOwnerVM = new VenueDetailsOwnerVM
                {
                    Id = venue.Id,
                    Name = venue.Name,
                    VenueType = venue.VenueType,
                    Address = venue.Address,
                    Country = venue.Country,
                    ZIP = venue.ZIP,
                    Description = venue.Description,
                    Capacity = venue.Capacity,
                    PricePerHour = venue.PricePerHour,
                    AirConditioningAvailable = venue.AirConditioningAvailable,
                    CateringAvailable = venue.CateringAvailable,
                    WifiAvailable = venue.WifiAvailable,
                    ParkingAvailable = venue.ParkingAvailable,
                    BarServiceAvailable = venue.BarServiceAvailable,
                    RestroomsAvailable = venue.RestroomsAvailable,
                    AudioVisualEquipment = venue.AudioVisualEquipment,
                    SpecialFeatures = venue.SpecialFeatures,
                    venuePhotos = venue.VenuePhotos.ToList(),
                    PendingEvents = pendingEventsList,
                    DateRange = bookedDates,
                    OwnerId = venue.OwnerId,
                    OwnerName = venue.Owner.UserName
                };
                return View("Details_Owner", venueDetailsOwnerVM);
            }
            else
            {
                var venueDetailsVM = new VenueDetailsVM
                {
                    Id = venue.Id,
                    Name = venue.Name,
                    VenueType = venue.VenueType,
                    Address = venue.Address,
                    Country = venue.Country,
                    ZIP = venue.ZIP,
                    Description = venue.Description,
                    Capacity = venue.Capacity,
                    PricePerHour = venue.PricePerHour,
                    AirConditioningAvailable = venue.AirConditioningAvailable,
                    CateringAvailable = venue.CateringAvailable,
                    WifiAvailable = venue.WifiAvailable,
                    ParkingAvailable = venue.ParkingAvailable,
                    BarServiceAvailable = venue.BarServiceAvailable,
                    RestroomsAvailable = venue.RestroomsAvailable,
                    AudioVisualEquipment = venue.AudioVisualEquipment,
                    SpecialFeatures = venue.SpecialFeatures,
                    venuePhotos = venue.VenuePhotos.ToList(),
                    DateRange = bookedDates
                };
                return View("Details", venueDetailsVM);
            }
        }



        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
