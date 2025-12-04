using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Controllers
{
    public static class UploadVenuePhoto
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
        public VenuesController(IVenueService venueManager)
        {
            _venueManager = venueManager;
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

            var venueCards = venues.Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    Type = v.VenueType.ToString(),
                    Address = v.Address!,
                    Capacity = v.Capacity,
                    PricePerHour = v.PricePerHour,
                    Photo = v.VenuePhotos.FirstOrDefault()!.PhotoUrl!
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

            var venueCards = venues.Select(v =>
                new VenueCardVM
                {
                    Id = v.Id,
                    VenueName = v.Name!,
                    Type = v.VenueType.ToString(),
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

            var bookedDates = venue.Events.Where(e => e.Status == Eventify.Models.Enums.EventStatusEnum.Approved || e.Status == Eventify.Models.Enums.EventStatusEnum.Paid)
                .Select(e => new DateRange
                {
                    StartDate = e.StartDateTime,
                    EndDate = e.EndDateTime
                }).ToList();

            var pendingEventsList = venue.Events
                .Where(e => e.Status == Eventify.Models.Enums.EventStatusEnum.Pending)
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

        public IActionResult Add()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Owner"))
            {
                return View(new VenueAddVM());
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(VenueAddVM vm)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString != null && User.IsInRole("Owner"))
            {
                List<string> uploadedFileNames = new List<string>();
                foreach (var x in vm.FormFiles)
                {
                    string p = UploadVenuePhoto.UploadFile("images", x);
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
                        ProofOfOwnership = $"/images/{ProofOfOwnershipPhoto}"
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



        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userIdString == null || !User.IsInRole("Owner"))
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }


        //    var venue = _venueManager.GetByIdWithPhotos(id);
        //    if (venue == null)
        //    {
        //        return NotFound();
        //    }

        //    if (venue.OwnerId.ToString() != userIdString)
        //    {
        //        return Forbid();
        //    }


        //    var vm = new VenueAddAndEditVM
        //    {
        //        Id = venue.Id,
        //        Name = venue.Name,
        //        VenueType = venue.VenueType,
        //        Address = venue.Address,
        //        City = venue.City,
        //        State = venue.State,
        //        ZIP = venue.ZIP,
        //        Description = venue.Description,
        //        Capacity = venue.Capacity,
        //        PricePerHour = venue.PricePerHour,
        //        SpecialFeatures = venue.SpecialFeatures,
        //        AirConditioningAvailable = venue.AirConditioningAvailable,
        //        CateringAvailable = venue.CateringAvailable,
        //        WifiAvailable = venue.WifiAvailable,
        //        ParkingAvailable = venue.ParkingAvailable,
        //        BarServiceAvailable = venue.BarServiceAvailable,
        //        RestroomsAvailable = venue.RestroomsAvailable,
        //        AudioVisualEquipment = venue.AudioVisualEquipment,
        //        ProofOfOwnership = venue.ProofOfOwnership,
        //        venuePhotos = venue.VenuePhotos.ToList()
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SaveEdit(VenueAddAndEditVM vm)
        //{
        //    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userIdString == null || !User.IsInRole("Owner"))
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }


        //    var venueToUpdate = _venueManager.GetByIdWithPhotos(vm.Id);
        //    if (venueToUpdate == null)
        //    {
        //        return NotFound();
        //    }
        //    if (venueToUpdate.OwnerId.ToString() != userIdString)
        //    {
        //        return Forbid();
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        if (vm.DeletedPhotos != null && vm.DeletedPhotos.Any())
        //        {
        //            foreach (var photoUrl in vm.DeletedPhotos)
        //            {
        //                var photoEntity = venueToUpdate.VenuePhotos
        //                    .FirstOrDefault(p => p.PhotoUrl == photoUrl);

        //                if (photoEntity != null)
        //                {

        //                    var fileName = photoUrl.Split('/').Last();
        //                    _fileService.RemoveFile("images", fileName);


        //                    venueToUpdate.VenuePhotos.Remove(photoEntity);
        //                }
        //            }
        //        }


        //        if (vm.FormFiles != null && vm.FormFiles.Any())
        //        {
        //            foreach (var file in vm.FormFiles)
        //            {
        //                string uniqueName = _fileService.SaveFile(file);

        //                venueToUpdate.VenuePhotos.Add(new VenuePhoto
        //                {
        //                    PhotoUrl = uniqueName,
        //                    VenueId = venueToUpdate.Id
        //                });
        //            }
        //        }

        //        if (vm.ProofOfOwnershipFile != null)
        //        {

        //            if (!string.IsNullOrEmpty(venueToUpdate.ProofOfOwnership))
        //            {

        //                var oldFileName = venueToUpdate.ProofOfOwnership.Split('/').Last();
        //                _fileService.RemoveFile("images", oldFileName);
        //            }


        //            venueToUpdate.ProofOfOwnership = _fileService.SaveFile(vm.ProofOfOwnershipFile);
        //        }
        //        else
        //        {

        //            venueToUpdate.ProofOfOwnership = vm.ProofOfOwnership;
        //        }


        //        venueToUpdate.Name = vm.Name;
        //        venueToUpdate.VenueType = vm.VenueType;
        //        venueToUpdate.Address = vm.Address;
        //        venueToUpdate.City = vm.City;
        //        venueToUpdate.State = vm.State;
        //        venueToUpdate.ZIP = vm.ZIP;
        //        venueToUpdate.Description = vm.Description;
        //        venueToUpdate.Capacity = vm.Capacity;
        //        venueToUpdate.PricePerHour = vm.PricePerHour;
        //        venueToUpdate.SpecialFeatures = vm.SpecialFeatures;
        //        venueToUpdate.AirConditioningAvailable = vm.AirConditioningAvailable;
        //        venueToUpdate.CateringAvailable = vm.CateringAvailable;
        //        venueToUpdate.WifiAvailable = vm.WifiAvailable;
        //        venueToUpdate.ParkingAvailable = vm.ParkingAvailable;
        //        venueToUpdate.BarServiceAvailable = vm.BarServiceAvailable;
        //        venueToUpdate.RestroomsAvailable = vm.RestroomsAvailable;
        //        venueToUpdate.AudioVisualEquipment = vm.AudioVisualEquipment;


        //        _venueManager.Update(venueToUpdate);

        //        return RedirectToAction("Details", new { id = venueToUpdate.Id });
        //    }
        //    var reloadedVenue = _venueManager.GetByIdWithPhotos(vm.Id);
        //    vm.venuePhotos = reloadedVenue?.VenuePhotos.ToList();

        //    return View("Edit", vm);
        //}




        public IActionResult Delete(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var venueToDelete = _venueManager.GetById(id);
            if (userIdString != null && venueToDelete.OwnerId.ToString() == userIdString)
            {
                int result = _venueManager.Delete(id);
                if (result == -1)
                {
                    TempData["ErrorMessage"] = "The venue cannot be deleted because it has paid events associated with it.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
