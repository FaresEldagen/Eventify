using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eventify.Controllers
{
    public class AdminController : Controller
    {
        IEventService _manager;
        private readonly IVenueService _venueManager;
        public AdminController(IEventService managerEvents, IVenueService venueService)
        {
            _manager = managerEvents;
            _venueManager = venueService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddAdmin()
        {
            return View();
        }
        public IActionResult ReviewUser()
        {
            return View();
        }
        public IActionResult VerifyUser()
        {
            return View();
        }
        public IActionResult DeclineUser()
        {
            return View();
        }
        public IActionResult ReviewVenue(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            var venue = _venueManager.GetByIdWithIncludes(id);
            if (venue == null || venue.VenueVerification != VenueVerification.Pending)
                return NotFound();

            var vm = new VenueEditVM
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
                SpecialFeatures = venue.SpecialFeatures,
                AirConditioningAvailable = venue.AirConditioningAvailable,
                CateringAvailable = venue.CateringAvailable,
                WifiAvailable = venue.WifiAvailable,
                ParkingAvailable = venue.ParkingAvailable,
                BarServiceAvailable = venue.BarServiceAvailable,
                RestroomsAvailable = venue.RestroomsAvailable,
                AudioVisualEquipment = venue.AudioVisualEquipment,
                ProofOfOwnership = venue.ProofOfOwnership,
                venuePhotos = venue.VenuePhotos.ToList()
            };
            return View(vm);
        }
        public IActionResult VerifyVenue(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            var venue = _venueManager.GetByIdWithIncludes(id);
            if (venue == null || venue.VenueVerification != VenueVerification.Pending)
                return NotFound();

            venue.VenueVerification = VenueVerification.Verified;
            _venueManager.Update(venue);
            return RedirectToAction("Index");
        }
        public IActionResult DeclineVenue(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            var venue = _venueManager.GetByIdWithIncludes(id);
            if (venue == null || venue.VenueVerification != VenueVerification.Pending)
                return NotFound();

            venue.VenueVerification = VenueVerification.Declined;
            _venueManager.Update(venue);
            return RedirectToAction("Index");
        }



        public IActionResult ReviewEvent(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            Event ev = _manager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            EventEditVM vm = new EventEditVM();
            vm.EventId = ev.EventId;
            vm.EventTitle = ev.EventTitle;
            vm.Category = ev.Category;
            vm.Description = ev.Description;
            vm.StartDateTime = ev.StartDateTime;
            vm.EndDateTime = ev.EndDateTime;
            vm.TicketPrice = ev.TicketPrice;
            vm.Features = ev.Features;
            vm.IsPrivate = ev.IsPrivate;
            vm.VenueId = ev.VenueId;
            foreach (var t in ev.EventPhotos)
            {
                vm.EventPhotos.Add(t);
            }
            return View(vm);
        }
        public IActionResult VerifyEvent(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            Event ev = _manager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Verified;
            _manager.Update(ev);
            return RedirectToAction("Index");
        }
        public IActionResult DeclineEvent(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            Event ev = _manager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Declined;
            _manager.Update(ev);
            return RedirectToAction("Index");
        }
    }
}
