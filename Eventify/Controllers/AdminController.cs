using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.AdminVm;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eventify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUserService _userManager;
        private readonly IEventService _eventManager;
        private readonly IVenueService _venueManager;
        private readonly IPaymentService _paymentManager;

        public AdminController(IApplicationUserService userManager, IEventService eventManager, IVenueService venueManager, IPaymentService paymentManager)
        {
            _userManager = userManager;
            _eventManager = eventManager;
            _venueManager = venueManager;
            _paymentManager = paymentManager;
        }

        public async Task<IActionResult> Index()
        {
            var adminDashboard = new AdminDashboardVM()
            {
                Admins = await _userManager.GetAllUserByRoleAsync("Admin"),
                Users = _userManager.GetApplicationUsers(),
                Events = _eventManager.GetPendingEvents(),
                Payments = _paymentManager.GetPayments(),
                Venues = _venueManager.GetPendingVenues(),
                UserEmail = null
            };
            return View(adminDashboard);
        }

        public async Task<IActionResult> AddAdmin(AdminDashboardVM dashboardVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.MakeUserAdminByEmailAsync(dashboardVM.UserEmail!);
                if (result == -1)
                    TempData["UserNotFoundError"] = true;
                else if (result == 0)
                    TempData["UserAlreadyAdminError"] = true;
                else if (result == -2)
                    TempData["SomethingWrongOccuredError"] = true;
            }

            return RedirectToAction("Index", "Admin");
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

            Event ev = _eventManager.GetByIdWithIncludes(id)!;
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

            Event ev = _eventManager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Verified;
            _eventManager.Update(ev);
            return RedirectToAction("Index");
        }
        public IActionResult DeclineEvent(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return RedirectToAction("Login", "Account");
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            Event ev = _eventManager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Declined;
            _eventManager.Update(ev);
            return RedirectToAction("Index");
        }
    }
}
