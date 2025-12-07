using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.AdminVm;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.ProfileVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _managerUser;

        public AdminController(IApplicationUserService userManager, IEventService eventManager, IVenueService venueManager, IPaymentService paymentManager, UserManager<ApplicationUser> managerUser)
        {
            _userManager = userManager;
            _eventManager = eventManager;
            _venueManager = venueManager;
            _paymentManager = paymentManager;
            _managerUser = managerUser;
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
        public async Task<IActionResult> ReviewUser(int id)
        {
            var user = await _managerUser.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            var profile = new EditProfileVM();
            profile.id = id;
            if (await _managerUser.IsInRoleAsync(user, "Owner"))
            {
                Owner OwnerUser = (Owner)user;
                profile.Photo = OwnerUser.Photo;
                profile.Gender = OwnerUser.Gender;
                profile.Country = OwnerUser.Country;
                profile.BIO = OwnerUser.BIO;
                profile.FrontIdPhoto = OwnerUser.FrontIdPhoto;
                profile.BackIdPhoto = OwnerUser.BackIdPhoto;
                profile.ArabicAddress = OwnerUser.ArabicAddress;
                profile.ArabicFullName = OwnerUser.ArabicFullName;
                profile.NationalIDNumber = OwnerUser.NationalIDNumber;
                profile.FrontIdPhoto = OwnerUser.FrontIdPhoto;
                profile.BackIdPhoto = OwnerUser.BackIdPhoto;
                profile.Photo = OwnerUser.Photo;
                profile.AccountStatus = OwnerUser.AccountStatus;
            }
            else
            {
                Organizer OrganizerUser = (Organizer)user;
                profile.Photo = OrganizerUser.Photo;
                profile.Gender = OrganizerUser.Gender;
                profile.Country = OrganizerUser.Country;
                profile.BIO = OrganizerUser.BIO;
                profile.ExperienceYear = OrganizerUser.ExperienceYear;
                profile.Specialization = OrganizerUser.Specialization;
                profile.FrontIdPhoto = OrganizerUser.FrontIdPhoto;
                profile.BackIdPhoto = OrganizerUser.BackIdPhoto;
                profile.ArabicAddress = OrganizerUser.ArabicAddress;
                profile.ArabicFullName = OrganizerUser.ArabicFullName;
                profile.NationalIDNumber = OrganizerUser.NationalIDNumber;
                profile.FrontIdPhoto = OrganizerUser.FrontIdPhoto;
                profile.BackIdPhoto = OrganizerUser.BackIdPhoto;
                profile.Photo = OrganizerUser.Photo;
                profile.AccountStatus = OrganizerUser.AccountStatus;
            }
            return View(profile);
        }
        public async Task<IActionResult> VerifyUser(int id)
        {
            var user = await _managerUser.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.AccountStatus = AccountStatus.Verified;
            await _managerUser.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeclineUser(int id)
        {
            var user = await _managerUser.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            user.AccountStatus = AccountStatus.Declined;
            await _managerUser.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        public IActionResult ReviewVenue(int id)
        {
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
            var venue = _venueManager.GetByIdWithIncludes(id);
            if (venue == null || venue.VenueVerification != VenueVerification.Pending)
                return NotFound();

            venue.VenueVerification = VenueVerification.Verified;
            _venueManager.Update(venue);
            return RedirectToAction("Index");
        }
        public IActionResult DeclineVenue(int id)
        {
            var venue = _venueManager.GetByIdWithIncludes(id);
            if (venue == null || venue.VenueVerification != VenueVerification.Pending)
                return NotFound();

            venue.VenueVerification = VenueVerification.Declined;
            _venueManager.Update(venue);
            return RedirectToAction("Index");
        }



        public IActionResult ReviewEvent(int id)
        {
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
            Event ev = _eventManager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Verified;
            _eventManager.Update(ev);
            return RedirectToAction("Index");
        }
        public IActionResult DeclineEvent(int id)
        {
            Event ev = _eventManager.GetByIdWithIncludes(id)!;
            if (ev == null || ev.EventVerification != EventVerification.Pending)
                return NotFound();

            ev.EventVerification = EventVerification.Declined;
            _eventManager.Update(ev);
            return RedirectToAction("Index");
        }
    }
}
