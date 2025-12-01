using Eventify.Controllers;
using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication2.Controllers
{

    public static class UploadEventPhoto
    {
        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                string FinalPath = Path.Combine(FolderPath, FileName);
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static string RemoveFile(string FolderName, string FileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, FileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Detected";
                }
                else
                {
                    return "FileNotFound";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
    


    public class EventsController : Controller
    {
        IEventService _manager;
        public EventsController(IEventService managerEvents)
        {
            _manager = managerEvents;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var events = _manager.GetByFilter_Search(
                title: null,
                sortBy: null,
                pageNumber: 1,
                category: null,
                maxPrice: null,
                startDate: null,
                endDate: null,
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
            var events = _manager.GetByFilter_Search(
                title: eventBrowseViewModel.Title,
                sortBy: (eventBrowseViewModel.SortBy==null)?null:eventBrowseViewModel.SortBy,
                pageNumber: (eventBrowseViewModel.PageNumber==0)?1:eventBrowseViewModel.PageNumber,
                category: (eventBrowseViewModel.Category==null)?null:eventBrowseViewModel.Category,
                maxPrice: eventBrowseViewModel.MaxPrice,
                startDate: eventBrowseViewModel.StartDate,
                endDate: eventBrowseViewModel.EndDate,
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


        [HttpGet]
        public IActionResult Add(int VenueId)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Organizer"))
            {
                EventAddOrEditVM vm = new EventAddOrEditVM();
                vm.VenueId = VenueId;
                return View(vm);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Add(EventAddOrEditVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Organizer"))
            {
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadEventPhoto.UploadFile("images", x);
                    EventPhoto eventPhoto = new EventPhoto();
                    eventPhoto.PhotoUrl = $"/images/" + p;
                    vm.EventPhotos.Add(eventPhoto);
                }
                if (ModelState.IsValid)
                {
                    Event ev = new Event
                    {
                        EventTitle = vm.EventTitle,
                        Category = vm.Category,
                        Description = vm.Description,
                        StartDateTime = vm.StartDateTime,
                        EndDateTime = vm.EndDateTime,
                        TicketPrice = vm.TicketPrice,
                        Features = vm.Features,
                        IsPrivate = vm.IsPrivate,
                        EventPhotos = vm.EventPhotos,
                        VenueId = vm.VenueId,
                        Status = EventStatusEnum.Pending
                    };

                    _manager.Insert(ev, userId);
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            return RedirectToAction("Login", "Account");
        }


        public IActionResult Details(int id)
        {
            Event ev = _manager.GetByIdWithIncludes(id);
            if (ev != null)
            {
                EventDetailsVM vm = new EventDetailsVM();
                vm.EventId = ev.EventId;
                vm.EventTitle = ev.EventTitle;
                vm.Description = ev.Description;
                vm.StartDateTime = ev.StartDateTime;
                vm.Status = ev.Status;
                vm.TicketPrice = ev.TicketPrice;
                vm.Features = ev.Features;
                vm.Address = ev.Address;
                vm.Category = ev.Category;
                vm.OrganizerName = ev.Organizer.UserName;
                vm.OrganizerId = ev.OrganizerId;
                vm.OwnerId = ev.Venue.OwnerId;
                vm.Capacity = ev.Capacity;
                vm.EventPhotos = ev.EventPhotos;
                return View(vm);

            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                int Id = int.Parse(userId);
                if (User.IsInRole("Organizer"))
                {
                    Event ev = _manager.GetByIdWithIncludes(id);
                    if (ev != null)
                    {
                        EventAddOrEditVM vm = new EventAddOrEditVM();
                        vm.EventId = ev.EventId;
                        vm.EventTitle = ev.EventTitle;
                        vm.Category = ev.Category;
                        vm.Description = ev.Description;
                        vm.StartDateTime = ev.StartDateTime;
                        vm.EndDateTime = ev.EndDateTime;
                        vm.TicketPrice = ev.TicketPrice;
                        vm.Features = ev.Features;
                        vm.IsPrivate = ev.IsPrivate;
                        foreach(var t in ev.EventPhotos)
                        {
                            vm.EventPhotos.Add(t);
                        }
                        return View(vm);
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Edit(EventAddOrEditVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                int Id = int.Parse(userId);
                if (User.IsInRole("Organizer"))
                {
                    if (ModelState.IsValid)
                    {
                        var ev = _manager.GetByIdWithIncludes(vm.EventId);
                        if (ev == null)
                        {
                            return NotFound();
                        }
                        ev.EventTitle = vm.EventTitle;
                        ev.Category = vm.Category;
                        ev.Description = vm.Description;
                        ev.StartDateTime = vm.StartDateTime;
                        ev.EndDateTime = vm.EndDateTime;
                        ev.TicketPrice = vm.TicketPrice;
                        ev.Features = vm.Features;
                        ev.IsPrivate = vm.IsPrivate;
                        ev.Status = EventStatusEnum.Pending;

                        if (vm.DeletedPhotos != null && vm.DeletedPhotos.Any())
                        {
                            foreach (var fileName in vm.DeletedPhotos)
                            {
                                UploadEventPhoto.RemoveFile("images", fileName);

                                var photoEntity = ev.EventPhotos
                                   .FirstOrDefault(p => p.PhotoUrl.EndsWith(fileName));

                                if (photoEntity != null)
                                    ev.EventPhotos.Remove(photoEntity);
                            }
                        }
                        if (vm.FormFiles != null && vm.FormFiles.Any())
                        {
                            foreach (var x in vm.FormFiles)
                            {
                                string p = UploadEventPhoto.UploadFile("images", x);

                                ev.EventPhotos.Add(new EventPhoto
                                {
                                    PhotoUrl = "/images/" + p
                                });
                            }
                        }
                        _manager.Update(ev);
                        return RedirectToAction("Index");
                    }
                    return View(vm);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Approval(int Id,EventStatusEnum decision)
        {
            Event ev = _manager.GetByIdWithIncludes(Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.Parse(userId) == ev.Venue.OwnerId)
            {
                ev.Status = decision;
                _manager.Update(ev);
            }
            return RedirectToAction("Details", "Events", new { id = Id });
        }


        public IActionResult Delete(int Id)
        {
            Event ev = _manager.GetByIdWithIncludes(Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.Parse(userId) == ev.OrganizerId)
            {
                _manager.Delete(Id);
            }
            return RedirectToAction("Index", "Events");
        }
    }
}
