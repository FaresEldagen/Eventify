using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class VenuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
