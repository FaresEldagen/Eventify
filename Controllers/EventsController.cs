using Eventify.Controllers;
using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication2.Controllers
{

    public static class UploadEventPhoto
    {
        public static string UploadFile(string FolderName, IFormFile File, int i)
        {
            try
            {
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;
                string FileName = $"{i}" + Guid.NewGuid() + Path.GetFileName(File.FileName);
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
        private readonly IEventService _manager;
        private readonly IVenueService _venueManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventsController(IEventService managerEvents, IVenueService venueService, UserManager<ApplicationUser> userManager)
        {
            _manager = managerEvents;
            _venueManager = venueService;
            _userManager = userManager;
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
                EventPhoto = (e.EventPhotos.Count > 0) ? e.EventPhotos[0].PhotoUrl! : "/images/default.jpg",
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
        public async Task<IActionResult> Add(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (!User.IsInRole("Organizer") || user.AccountStatus != AccountStatus.Verified)
            {
                TempData["Cant'nAddEventError"] = true;
                return RedirectToAction("Index","Profile");
            }

            EventAddVM vm = new EventAddVM();
            vm.VenueId = id;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EventAddVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Organizer"))
            {
                List<string> uploadedFileNames = new List<string>();
                int i = 0;
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadEventPhoto.UploadFile("images", x, i++);
                    EventPhoto eventPhoto = new EventPhoto();
                    eventPhoto.PhotoUrl = $"/images/" + p;
                    uploadedFileNames.Add(p);
                    vm.EventPhotos.Add(eventPhoto);
                }
                var venue = _venueManager.GetByIdWithIncludes(vm.VenueId);
                var venueEventsDates = venue.Events.Select(e => new { e.StartDateTime, e.EndDateTime });

                var isInvalid = false;

                foreach (var e in venueEventsDates)
                {
                    if(vm.StartDateTime <= e.EndDateTime &&
                                    vm.EndDateTime >= e.StartDateTime)
                    {
                        isInvalid = true;
                        break;
                    }
                }

                if (ModelState.IsValid && !isInvalid)
                {
                    Event ev = new Event
                    {
                        OrganizerId = int.Parse(userId),
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
                        Status = EventStatusEnum.Pending,
                        EventVerification = EventVerification.Pending
                        
                    };

                    _manager.Insert(ev);
                    return RedirectToAction("Index");
                }
                else 
                {
                    foreach (var file in uploadedFileNames)
                    {
                        UploadEventPhoto.RemoveFile("images", file);
                    }
                    vm.EventPhotos.Clear();

                    if (isInvalid)
                        TempData["Selected Date is Invalid"] = true;

                    return View(vm);
                }
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
                vm.EndDateTime = ev.EndDateTime;
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
                vm.EventVerification = ev.EventVerification;
                return View(vm);

            }
            return NotFound();
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Event ev = _manager.GetByIdWithIncludes(id)!;

            if (ev.Status == EventStatusEnum.Paid)
            {
                TempData["EditEventError"] = true;
                return RedirectToAction("Details", "Events", new { id = id });
            }
            if (userId != null && ev != null && ev.OrganizerId.ToString() == userId)
            {
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
                foreach(var t in ev.EventPhotos)
                {
                    vm.EventPhotos.Add(t);
                }
                return View(vm); 
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventEditVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Event ev = _manager.GetByIdWithIncludes(vm.EventId);
            if (userId != null && ev != null && ev.OrganizerId.ToString() == userId)
            {
                int i = 0;
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadEventPhoto.UploadFile("images", x, i++);
                    EventPhoto eventPhoto = new EventPhoto();
                    eventPhoto.PhotoUrl = $"/images/" + p;
                    vm.EventPhotos.Add(eventPhoto);
                }
                var isInvalid = false;
                if (vm.VenueId != null)
                {
                    int venueId = vm.VenueId ?? 0;
                    var venue = _venueManager.GetByIdWithIncludes(venueId);
                    var venueEventsDates = venue.Events.Where(e => e.EventId != vm.EventId && (e.Status == EventStatusEnum.Approved || e.Status == EventStatusEnum.Paid))
                        .Select(e => new { e.StartDateTime, e.EndDateTime });


                    foreach (var e in venueEventsDates)
                    {
                        if (vm.StartDateTime < e.EndDateTime &&
                                        vm.EndDateTime > e.StartDateTime)
                        {
                            isInvalid = true;
                            break;
                        }
                    }
                }
                if (ModelState.IsValid && !isInvalid)
                {
                    ev.EventTitle = vm.EventTitle;
                    ev.Category = vm.Category;
                    ev.Description = vm.Description;
                    ev.StartDateTime = vm.StartDateTime;
                    ev.EndDateTime = vm.EndDateTime;
                    ev.TicketPrice = vm.TicketPrice;
                    ev.Features = vm.Features;
                    ev.IsPrivate = vm.IsPrivate;
                    ev.Status = EventStatusEnum.Pending;
                    ev.EventVerification = EventVerification.Pending;
                    ev.Organizer.PastEventCount -= 1;

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
                        i = 0;
                        foreach (var x in vm.FormFiles)
                        {
                            string p = UploadEventPhoto.UploadFile("images", x,i++);

                            ev.EventPhotos.Add(new EventPhoto
                            {
                                PhotoUrl = "/images/" + p
                            });
                        }
                    }
                    _manager.Update(ev);
                    return RedirectToAction("Index");
                }

                var reloadedVenue = _manager.GetByIdWithIncludes(vm.EventId);
                vm.EventPhotos = reloadedVenue.EventPhotos.ToList();
                if (isInvalid)
                    TempData["Selected Date is Invalid"] = true;
                return View(vm);
            }
            return RedirectToAction("Login", "Account");
        }



        public IActionResult Approval(int Id,EventStatusEnum decision)
        {
            Event ev = _manager.GetByIdWithIncludes(Id);
            if (ev == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.Parse(userId) == ev.Venue.OwnerId)
            {
                ev.Status = decision;
                if (decision == EventStatusEnum.Approved)
                {
                    ev.Organizer.PastEventCount += 1;
                }
                _manager.Update(ev);
            }
            return RedirectToAction("Details", "Events", new { id = Id });
        }



        public IActionResult Delete(int Id)
        {
            Event ev = _manager.GetByIdWithIncludes(Id);
            if (ev == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && int.Parse(userId) == ev.OrganizerId)
            {
                if (ev.Status == EventStatusEnum.Paid)
                {
                    TempData["DeleteEventError"] = true;
                    return RedirectToAction("Details", "Events", new { id = Id });
                }
                if (ev.Status == EventStatusEnum.Approved || ev.Status == EventStatusEnum.Finished)
                {
                    ev.Organizer.PastEventCount -= 1;
                    _manager.Update(ev);
                }
                _manager.Delete(Id);
            }
            return RedirectToAction("Index", "Events");
        }
    }
}
