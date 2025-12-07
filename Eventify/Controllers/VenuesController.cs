using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Controllers
{
    public static class UploadVenuePhoto
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

                if (System.IO.File.Exists(directory))
                {
                    System.IO.File.Delete(directory);
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



    public class VenuesController : Controller
    {
        private readonly IVenueService _venueManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public VenuesController(IVenueService venueManager, UserManager<ApplicationUser> userManager)
        {
            _venueManager = venueManager;
            _userManager = userManager;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var venues = _venueManager.GetByFilter_Search
                (
                    sortBy: null,
                    sortingType: SortingTypeEnum.Ascending,
                    pageNumber: 1,
                    title: null,
                    venueType: null,
                    maxprice: null,
                    country: null,
                    airConditioning: null,
                    catering: null,
                    wifi: null,
                    parking: null,
                    barService: null,
                    restrooms: null,
                    audioVisual: null,
                    out int totalVenues
                );

            var venueCards = venues.Where(v=>v.VenueVerification == VenueVerification.Verified).Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    VenueType = v.VenueType.ToString(),
                    Address = v.Address!,
                    Capacity = v.Capacity,
                    PricePerHour = v.PricePerHour,
                    Photo = (v.VenuePhotos.Count > 0)? v.VenuePhotos[0].PhotoUrl! : "/images/default.jpg"
                }).ToList();

            var totalFilterVenues = totalVenues;

            //ViewBag.VenueCards = venueCards;
            var venueViewModel = new VenueBrowseViewModel();
            venueViewModel.venueCards = venueCards;
            venueViewModel.TotalPages = (int)Math.Ceiling(totalVenues / 9.0m);
            return View("Index", venueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(VenueBrowseViewModel venueBrowseViewModel)
        {
            var venues = _venueManager.GetByFilter_Search
                (
                    sortBy: (venueBrowseViewModel.sortBy == null) ? null : (SortByEnum)venueBrowseViewModel.sortBy,
                    sortingType: (venueBrowseViewModel.sortingType == null) ? SortingTypeEnum.Ascending : (SortingTypeEnum)venueBrowseViewModel.sortingType,
                    pageNumber: venueBrowseViewModel.pageNumber == 0 ? 1 : venueBrowseViewModel.pageNumber,
                    title: venueBrowseViewModel.title,
                    venueType: (venueBrowseViewModel.venueType == null) ? null : (VenueTypeEnum)venueBrowseViewModel.venueType,
                    maxprice: venueBrowseViewModel.maxprice,
                    country: venueBrowseViewModel.country,
                    airConditioning: venueBrowseViewModel.airConditioning,
                    catering: venueBrowseViewModel.catering,
                    wifi: venueBrowseViewModel.wifi,
                    parking: venueBrowseViewModel.parking,
                    barService: venueBrowseViewModel.barService,
                    restrooms: venueBrowseViewModel.restrooms,
                    audioVisual: venueBrowseViewModel.audioVisual,
                    out int totalVenues
                );

            var venueCards = venues.Where(v => v.VenueVerification == VenueVerification.Verified).Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    VenueType = v.VenueType.ToString(),
                    Address = v.Address!,
                    Capacity = v.Capacity,
                    PricePerHour = v.PricePerHour,
                    Photo = v.VenuePhotos.FirstOrDefault()!.PhotoUrl!
                }).ToList();

            var totalFilterVenues = totalVenues;

            venueBrowseViewModel.venueCards = venueCards;
            venueBrowseViewModel.TotalPages = (int)Math.Ceiling(totalFilterVenues / 9.0m);
            //return View(venueBrowseViewModel);
            return PartialView("_SearchVenues", venueBrowseViewModel);
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var venue = _venueManager.GetByIdWithIncludes(id);

            if (venue == null)
            {
                return NotFound();
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdString, out int currentUserId);

            var bookedDates = venue.Events.Where(e => (e.Status == EventStatusEnum.Approved || e.Status == EventStatusEnum.Paid) && e.EventVerification == EventVerification.Verified)
                .Select(e => new DateRange
                {
                    StartDate = e.StartDateTime,
                    EndDate = e.EndDateTime
                }).ToList();

            var pendingEventsList = venue.Events
                .Where(e => e.Status == EventStatusEnum.Pending)
                .ToList();

            if (User.IsInRole("Owner") && venue.OwnerId == currentUserId)
            {
                var venueDetailsOwnerVM = new VenueDetailsOwnerVM
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
                    AirConditioningAvailable = venue.AirConditioningAvailable,
                    CateringAvailable = venue.CateringAvailable,
                    WifiAvailable = venue.WifiAvailable,
                    ParkingAvailable = venue.ParkingAvailable,
                    BarServiceAvailable = venue.BarServiceAvailable,
                    RestroomsAvailable = venue.RestroomsAvailable,
                    AudioVisualEquipment = venue.AudioVisualEquipment,
                    SpecialFeatures = venue.SpecialFeatures,
                    venuePhotos = venue.VenuePhotos.ToList(),
                    PendingEvents = pendingEventsList,
                    DateRange = bookedDates,
                    OwnerId = venue.OwnerId,
                    VenueVerification = venue.VenueVerification,
                    OwnerName = venue.Owner.UserName
                };
                return View("Details_Owner", venueDetailsOwnerVM);
            }
            else
            {
                var venueDetailsVM = new VenueDetailsVM
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
                    AirConditioningAvailable = venue.AirConditioningAvailable,
                    CateringAvailable = venue.CateringAvailable,
                    WifiAvailable = venue.WifiAvailable,
                    ParkingAvailable = venue.ParkingAvailable,
                    BarServiceAvailable = venue.BarServiceAvailable,
                    RestroomsAvailable = venue.RestroomsAvailable,
                    AudioVisualEquipment = venue.AudioVisualEquipment,
                    SpecialFeatures = venue.SpecialFeatures,
                    venuePhotos = venue.VenuePhotos.ToList(),
                    DateRange = bookedDates
                };
                return View("Details", venueDetailsVM);
            }
        }



        [HttpGet]

        public async Task<IActionResult> Add()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (!User.IsInRole("Owner") || user.AccountStatus != AccountStatus.Verified)
                return RedirectToAction("Index", "Profile");

            return View(new VenueAddVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(VenueAddVM vm)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString != null && User.IsInRole("Owner"))
            {
                List<string> uploadedFileNames = new List<string>();
                int i = 0;
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadVenuePhoto.UploadFile("images", x, i++);
                    VenuePhoto venuePhoto = new VenuePhoto();
                    venuePhoto.PhotoUrl = $"/images/" + p;
                    uploadedFileNames.Add(p);
                    vm.venuePhotos.Add(venuePhoto);
                }
                if (ModelState.IsValid)
                {
                    var ProofOfOwnershipPhoto = UploadProfilePhoto.UploadFile("images", vm.ProofOfOwnershipFile);
                    var venue = new Venue
                    {
                        OwnerId = int.Parse(userIdString),
                        Name = vm.Name!,
                        VenueType = vm.VenueType,
                        Address = vm.Address!,
                        Country = vm.Country,
                        ZIP = vm.ZIP,
                        Description = vm.Description,
                        Capacity = vm.Capacity,
                        PricePerHour = vm.PricePerHour,
                        SpecialFeatures = vm.SpecialFeatures,
                        AirConditioningAvailable = vm.AirConditioningAvailable,
                        CateringAvailable = vm.CateringAvailable,
                        WifiAvailable = vm.WifiAvailable,
                        ParkingAvailable = vm.ParkingAvailable,
                        BarServiceAvailable = vm.BarServiceAvailable,
                        RestroomsAvailable = vm.RestroomsAvailable,
                        AudioVisualEquipment = vm.AudioVisualEquipment,

                        VenuePhotos = vm.venuePhotos,
                        ProofOfOwnership = $"/images/{ProofOfOwnershipPhoto}",
                        VenueVerification = VenueVerification.Pending
                        
                    };
                    _venueManager.Insert(venue);
                    return RedirectToAction("Details", new { id = venue.Id });
                }
                else
                {
                    foreach (var file in uploadedFileNames)
                    {
                        UploadEventPhoto.RemoveFile("images", file);
                    }
                    vm.venuePhotos.Clear();
                    return View(vm);
                }
            }
            return RedirectToAction("Login", "Account");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var venue = _venueManager.GetByIdWithIncludes(id);
            if (userIdString != null && venue.OwnerId.ToString() == userIdString)
            {
                if (venue.Events.Any(v => v.Status == EventStatusEnum.Paid))
                {
                    TempData["EditVenueError"] = true;
                    return RedirectToAction("Details", "Venues", new { id = id });
                }

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
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VenueEditVM vm)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var venueToUpdate = _venueManager.GetByIdWithIncludes(vm.Id);
            if (userIdString != null && venueToUpdate.OwnerId.ToString() == userIdString)
            {
                List<string> uploadedFileNames = new List<string>();
                int i = 0;
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadVenuePhoto.UploadFile("images", x, i++);
                    VenuePhoto venuePhoto = new VenuePhoto();
                    venuePhoto.PhotoUrl = $"/images/" + p;
                    uploadedFileNames.Add(p);
                    vm.venuePhotos.Add(venuePhoto);
                }
                if (ModelState.IsValid)
                {
                    venueToUpdate.Name = vm.Name;
                    venueToUpdate.VenueType = vm.VenueType;
                    venueToUpdate.Address = vm.Address;
                    venueToUpdate.Country = vm.Country;
                    venueToUpdate.ZIP = vm.ZIP;
                    venueToUpdate.Description = vm.Description;
                    venueToUpdate.Capacity = vm.Capacity;
                    venueToUpdate.PricePerHour = vm.PricePerHour;
                    venueToUpdate.SpecialFeatures = vm.SpecialFeatures;
                    venueToUpdate.AirConditioningAvailable = vm.AirConditioningAvailable;
                    venueToUpdate.CateringAvailable = vm.CateringAvailable;
                    venueToUpdate.WifiAvailable = vm.WifiAvailable;
                    venueToUpdate.ParkingAvailable = vm.ParkingAvailable;
                    venueToUpdate.BarServiceAvailable = vm.BarServiceAvailable;
                    venueToUpdate.RestroomsAvailable = vm.RestroomsAvailable;
                    venueToUpdate.AudioVisualEquipment = vm.AudioVisualEquipment;

                    if (vm.DeletedPhotos != null && vm.DeletedPhotos.Any())
                    {
                        foreach (var fileName in vm.DeletedPhotos)
                        {
                            UploadEventPhoto.RemoveFile("images", fileName);

                            var photoEntity = venueToUpdate.VenuePhotos
                                .FirstOrDefault(p => p.PhotoUrl.EndsWith(fileName));

                            if (photoEntity != null)
                                venueToUpdate.VenuePhotos.Remove(photoEntity);
                        }
                    }

                    if (vm.FormFiles != null && vm.FormFiles.Any())
                    {
                        i = 0;
                        foreach (var x in vm.FormFiles)
                        {
                            string p = UploadEventPhoto.UploadFile("images", x, i++);

                            venueToUpdate.VenuePhotos.Add(new VenuePhoto
                            {
                                PhotoUrl = "/images/" + p
                            });
                        }
                    }

                    if (vm.RemoveProofOfOwnershipPhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{venueToUpdate.ProofOfOwnership}");
                        venueToUpdate.ProofOfOwnership = null;
                    }
                    else if (vm.ProofOfOwnershipFile != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", vm.ProofOfOwnershipFile);
                        venueToUpdate.ProofOfOwnership = $"/images/{PhotoName}";
                    }

                    venueToUpdate.VenueVerification = VenueVerification.Pending;

                    _venueManager.Update(venueToUpdate);
                    return RedirectToAction("Details", new { id = venueToUpdate.Id });
                }
                var reloadedVenue = _venueManager.GetByIdWithIncludes(vm.Id);
                vm.venuePhotos = reloadedVenue.VenuePhotos.ToList();
                vm.ProofOfOwnership = reloadedVenue.ProofOfOwnership;
                return View(vm);
            }
            return RedirectToAction("Login", "Account");
        }



        public IActionResult Delete(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var venueToDelete = _venueManager.GetByIdWithIncludes(id);
            if (userIdString != null && venueToDelete.OwnerId.ToString() == userIdString)
            {
                if (venueToDelete.Events.Any(v => v.Status == EventStatusEnum.Paid))
                {
                    TempData["DeleteVenueError"] = true;
                    return RedirectToAction("Details", "Venues",new { id = id });
                }

                int result = _venueManager.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
