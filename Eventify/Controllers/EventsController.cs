using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var events = _eventService.GetByFilter_Search(
                title: null,
                sortBy: null,
                pageNumber: 1,
                category: null,
                maxPrice: null,
                startDate: null,
                endDate: null,
                isPrivate: null,
                out int totalEvents
                );

            var eventCards = events.Select(e => new EventCardVM
            {
                Id = e.EventId,
                EventTitle = e.EventTitle!,
                Category = e.Category.ToString(),
                EventPhoto = e.EventPhotos.FirstOrDefault()!.PhotoUrl!,
                Address = e.Address!,
                StartDateTime = e.StartDateTime.ToString(),
                Status = e.Status.ToString(),
                TicketPrice = e.TicketPrice
            }).ToList();

            EventBrowseViewModel eventBrowseViewModel = new EventBrowseViewModel();
            eventBrowseViewModel.EventCards = eventCards;
            eventBrowseViewModel.TotalPages = (int)Math.Ceiling(totalEvents / 9.0m);

            return View("Index",eventBrowseViewModel);
        }

        [HttpPost]
        public IActionResult Index(EventBrowseViewModel eventBrowseViewModel)
        {
            var events = _eventService.GetByFilter_Search(
                title: eventBrowseViewModel.Title,
                sortBy: (eventBrowseViewModel.SortBy==null)?null:eventBrowseViewModel.SortBy,
                pageNumber: (eventBrowseViewModel.PageNumber==0)?1:eventBrowseViewModel.PageNumber,
                category: (eventBrowseViewModel.Category==null)?null:eventBrowseViewModel.Category,
                maxPrice: eventBrowseViewModel.MaxPrice,
                startDate: eventBrowseViewModel.StartDate,
                endDate: eventBrowseViewModel.EndDate,
                isPrivate: eventBrowseViewModel.IsPrivate,
                out int totalEvents
                );

            var eventCards = events.Select(e => new EventCardVM
            {
                Id = e.EventId,
                EventTitle = e.EventTitle!,
                Category = e.Category.ToString(),
                EventPhoto = e.EventPhotos.FirstOrDefault()!.PhotoUrl!,
                Address = e.Address!,
                StartDateTime = e.StartDateTime.ToString(),
                Status = e.Status.ToString(),
                TicketPrice = e.TicketPrice
            }).ToList();

            eventBrowseViewModel.EventCards = eventCards;
            eventBrowseViewModel.TotalPages = (int)Math.Ceiling(totalEvents / 9.0m);

            return PartialView("_SearchEvents", eventBrowseViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
