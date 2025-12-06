using Eventify.Models.Entities;
using Eventify.Services;
using Eventify.ViewModels.AdminVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventify.Controllers
{
    [Authorize(Roles ="Admin")]
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
                Events= _eventManager.GetPendingEvents(),
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

            return RedirectToAction("Index","Admin");
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
        public IActionResult ReviewVenue()
        {
            return View();
        }
        public IActionResult VerifyVenue()
        {
            return View();
        }
        public IActionResult DeclineVenue()
        {
            return View();
        }
        public IActionResult ReviewEvent()
        {
            return View();
        }
        public IActionResult VerifyEvent()
        {
            return View();
        }
        public IActionResult DeclineEvent()
        {
            return View();
        }
    }
}
