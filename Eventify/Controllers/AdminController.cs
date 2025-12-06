using Microsoft.AspNetCore.Mvc;

namespace Eventify.Controllers
{
    public class AdminController : Controller
    {
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
