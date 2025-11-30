using Eventify.Managers;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Mvc;
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
                                city: null,
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
            return View("Index",venueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // لو تستخدم AntiForgery
        public IActionResult Index(VenueBrowseViewModel venueBrowseViewModel)
        {
            var venues = _venueManager.GetByFilter_Search
                            (
                                sortBy: (venueBrowseViewModel.sortBy==null)?null: (SortByEnum)venueBrowseViewModel.sortBy,
                                sortingType: (venueBrowseViewModel.sortingType == null)? SortingTypeEnum.Ascending : (SortingTypeEnum)venueBrowseViewModel.sortingType,
                                pageNumber: venueBrowseViewModel.pageNumber==0?1: venueBrowseViewModel.pageNumber,
                                title: venueBrowseViewModel.title,
                                venueType: (venueBrowseViewModel.venueType == null) ? null : (VenueTypeEnum)venueBrowseViewModel.venueType,
                                maxprice: venueBrowseViewModel.maxprice,
                                city: venueBrowseViewModel.city,
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

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Details_Owner()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
