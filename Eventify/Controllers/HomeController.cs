using System;
using System.Diagnostics;
using Eventify.Models;
using Eventify.Models.Entities;
using Eventify.Services;
using Eventify.ViewModels;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eventify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IVenueService _managerVenues;
        IEventService _managerEvents;
        UserManager<ApplicationUser> _managerUser;

        public HomeController(ILogger<HomeController> logger, IVenueService managerVenues, IEventService managerEvents, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _managerVenues = managerVenues;
            _managerEvents = managerEvents;
            _managerUser = managerUser;
        }

        public IActionResult Index()
        {
            List<Event> Events = _managerEvents.Get3();
            List<Venue> Venues = _managerVenues.Get3();
            List<EventCardVM> EventsCards = new List<EventCardVM>();
            List<VenueCardVM> VenuesCards = new List<VenueCardVM>();

            foreach (Event event_ in Events)
            {
                EventCardVM eventcard = new EventCardVM();
                eventcard.Id = event_.EventId;
                eventcard.EventTitle = event_.EventTitle;
                eventcard.TicketPrice = event_.TicketPrice;
                eventcard.Category = event_.Category.ToString();
                eventcard.Address = event_.Address;
                eventcard.Status = event_.Status.ToString();
                if (event_.EventPhotos.Count > 0)
                {
                    eventcard.EventPhoto = event_.EventPhotos[0].PhotoUrl;
                }
                else
                {
                    eventcard.EventPhoto = "/images/default.jpg";
                }
                eventcard.StartDateTime = event_.StartDateTime.ToShortDateString();
                EventsCards.Add(eventcard);
            }

            foreach (Venue venue in Venues)
            {
                VenueCardVM venuecard = new VenueCardVM();
                venuecard.Id = venue.Id;
                venuecard.VenueName = venue.Name;
                venuecard.PricePerHour = venue.PricePerHour;
                venuecard.Capacity = venue.Capacity;
                venuecard.VenueType = venue.VenueType.ToString();
                venuecard.Address = venue.Address;
                if (venue.VenuePhotos.Count > 0)
                {
                    venuecard.Photo = venue.VenuePhotos[0].PhotoUrl;
                }
                else
                {
                    venuecard.Photo = "/images/default.jpg";
                }
                VenuesCards.Add(venuecard);
            }

            var viewModel = new HomepageVewModel
            {
                UpcomingEvents = EventsCards,
                TopVenues = VenuesCards
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
