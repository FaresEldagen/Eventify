using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
