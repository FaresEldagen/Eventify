using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
