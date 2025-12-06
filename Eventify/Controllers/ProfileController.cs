using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.ProfileVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication2.Controllers
{
    public static class UploadProfilePhoto
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
        public static string RemoveFile(string FileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), FileName);

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
    public class ProfileController : Controller
    {
        IVenueService _managerVenues;
        IEventService _managerEvents;
        UserManager<ApplicationUser> _managerUser;
        public ProfileController(IVenueService managerVenues, IEventService managerEvents, UserManager<ApplicationUser> managerUser)
        {
            _managerVenues = managerVenues;
            _managerEvents = managerEvents;
            _managerUser = managerUser;
        }



        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                int Id = int.Parse(userId);
                if (User.IsInRole("Owner"))
                {
                    var user = (Owner)await _managerUser.FindByIdAsync(userId);
                    List<Venue> venues = _managerVenues.GetByUserId(Id);
                    List<VenueCardVM> venuesCards = new List<VenueCardVM>();
                    foreach (Venue venue in venues)
                    {
                        VenueCardVM venuecard = new VenueCardVM();
                        venuecard.Id = venue.Id;
                        venuecard.VenueName = venue.Name;
                        venuecard.PricePerHour = venue.PricePerHour;
                        venuecard.Capacity = venue.Capacity;
                        venuecard.VenueType = venue.VenueType.ToString();
                        venuecard.Address = venue.Address;
                        venuecard.Status = venue.VenueVerification.ToString();
                        if (venue.VenuePhotos.Count > 0)
                            venuecard.Photo = venue.VenuePhotos[0].PhotoUrl;
                        else
                            venuecard.Photo = "/images/default.jpg";
                        venuesCards.Add(venuecard);
                    }

                    var profile = new PersonalVenueOwnerVM();
                    profile.Id = Id;
                    profile.Name = user.UserName;
                    profile.Email = user.Email;
                    profile.JoinedDate = user.JoinedDate.ToShortDateString();
                    profile.Gender = string.IsNullOrWhiteSpace(user.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                    profile.BIO = user.BIO;
                    profile.Photo = user.Photo;
                    profile.Country = string.IsNullOrWhiteSpace(user.Country.ToString()) ? "Undefined" : user.Country.ToString();
                    profile.VenueCount = venuesCards.Count;
                    profile.Venues = venuesCards;
                    profile.WithdrawableEarnings = user.WithdrawableEarnings;
                    profile.Verfication = user.AccountStatus.ToString();
                    return View("PersonalVenueOwner", profile);
                }
                else if(User.IsInRole("Organizer"))
                {
                    var user = (Organizer)await _managerUser.FindByIdAsync(userId);
                    List<Event> events = _managerEvents.GetByUserId(Id);
                    List<EventCardVM> EventCards = new List<EventCardVM>();
                    foreach (Event event_ in events)
                    {
                        EventCardVM eventcard = new EventCardVM();
                        eventcard.Id = event_.EventId;
                        eventcard.EventTitle = event_.EventTitle;
                        eventcard.TicketPrice = event_.TicketPrice;
                        eventcard.Category = event_.Category.ToString();
                        eventcard.Address = event_.Address;
                        eventcard.Status = event_.Status.ToString();
                        if (event_.EventPhotos.Count > 0)
                            eventcard.EventPhoto = event_.EventPhotos[0].PhotoUrl;
                        else
                            eventcard.EventPhoto = "/images/default.jpg";
                        eventcard.StartDateTime = event_.StartDateTime.ToShortDateString();
                        EventCards.Add(eventcard);
                    }

                    var profile = new EventOrganizerVM();
                    profile.Id = Id;
                    profile.Name = user.UserName;
                    profile.Email = user.Email;
                    profile.JoinedDate = user.JoinedDate.ToShortDateString();
                    profile.Gender = string.IsNullOrWhiteSpace(user.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                    profile.BIO = user.BIO;
                    profile.Photo = user.Photo;
                    profile.Country = string.IsNullOrWhiteSpace(user.Country.ToString()) ? "Undefined" : user.Country.ToString();
                    profile.Specialization = user.Specialization ?? "Undefined";
                    profile.ExperienceYear = user.ExperienceYear;
                    profile.EventsCount = EventCards.Count;
                    profile.Events = EventCards;
                    profile.Verfication = user.AccountStatus.ToString();
                    return View("PersonalEventOrganizer", profile);
                }
                else if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> View(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && Id == int.Parse(userId))
            {
                return RedirectToAction("Index");
            }
            else if (userId != null)
            {
                var user = await _managerUser.FindByIdAsync(Id.ToString());
                if (await _managerUser.IsInRoleAsync(user, "Owner"))
                {
                    Owner OwnerUser = (Owner)user;
                    List<Venue> venues = _managerVenues.GetByUserId(Id);
                    List<VenueCardVM> venuesCards = new List<VenueCardVM>();
                    foreach (Venue venue in venues)
                    {
                        VenueCardVM venuecard = new VenueCardVM();
                        venuecard.Id = venue.Id;
                        venuecard.VenueName = venue.Name;
                        venuecard.PricePerHour = venue.PricePerHour;
                        venuecard.Capacity = venue.Capacity;
                        venuecard.VenueType = venue.VenueType.ToString();
                        venuecard.Address = venue.Address;
                        venuecard.Status = venue.VenueVerification.ToString();
                        if (venue.VenuePhotos.Count > 0)
                            venuecard.Photo = venue.VenuePhotos[0].PhotoUrl;
                        else
                            venuecard.Photo = "/images/default.jpg";
                        venuesCards.Add(venuecard);
                    }

                    var profile = new OtherVenueOwnerVM();
                    profile.Id = Id;
                    profile.Name = OwnerUser.UserName;
                    profile.Email = OwnerUser.Email;
                    profile.JoinedDate = OwnerUser.JoinedDate.ToShortDateString();
                    profile.Gender = string.IsNullOrWhiteSpace(OwnerUser.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                    profile.BIO = OwnerUser.BIO;
                    profile.Photo = OwnerUser.Photo;
                    profile.Country = string.IsNullOrWhiteSpace(OwnerUser.Country.ToString()) ? "Undefined" : user.Country.ToString();
                    profile.VenueCount = venuesCards.Count;
                    profile.Venues = venuesCards;
                    profile.WithdrawableEarnings = OwnerUser.WithdrawableEarnings;
                    return View("OtherVenueOwner", profile);
                }
                else if (await _managerUser.IsInRoleAsync(user, "Organizer"))
                {
                    Organizer OrganizerUser = (Organizer)user;
                    List<Event> events = _managerEvents.GetByUserId(Id);
                    List<EventCardVM> EventCards = new List<EventCardVM>();
                    foreach (Event event_ in events)
                    {
                        EventCardVM eventcard = new EventCardVM();
                        eventcard.Id = event_.EventId;
                        eventcard.EventTitle = event_.EventTitle;
                        eventcard.TicketPrice = event_.TicketPrice;
                        eventcard.Category = event_.Category.ToString();
                        eventcard.Address = event_.Address;
                        eventcard.Status = event_.Status.ToString();
                        if (event_.EventPhotos.Count > 0)
                            eventcard.EventPhoto = event_.EventPhotos[0].PhotoUrl;
                        else
                            eventcard.EventPhoto = "/images/default.jpg";
                        eventcard.StartDateTime = event_.StartDateTime.ToShortDateString();
                        EventCards.Add(eventcard);
                    }

                    var profile = new EventOrganizerVM();
                    profile.Id = Id;
                    profile.Name = OrganizerUser.UserName;
                    profile.Email = OrganizerUser.Email;
                    profile.JoinedDate = OrganizerUser.JoinedDate.ToShortDateString();
                    profile.Gender = string.IsNullOrWhiteSpace(OrganizerUser.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                    profile.BIO = OrganizerUser.BIO;
                    profile.Photo = OrganizerUser.Photo;
                    profile.Country = string.IsNullOrWhiteSpace(OrganizerUser.Country.ToString()) ? "Undefined" : user.Country.ToString();
                    profile.Specialization = OrganizerUser.Specialization ?? "Undefined";
                    profile.ExperienceYear = OrganizerUser.ExperienceYear;
                    profile.EventsCount = EventCards.Count;
                    profile.Events = EventCards;
                    return View("OtherEventOrganizer", profile);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var user = await _managerUser.FindByIdAsync(userId);
                var profile = new EditProfileVM();
                if (await _managerUser.IsInRoleAsync(user, "Owner"))
                {
                    Owner OwnerUser = (Owner)user;
                    profile.Photo = OwnerUser.Photo;
                    profile.Gender = OwnerUser.Gender;
                    profile.Country = OwnerUser.Country;
                    profile.BIO = OwnerUser.BIO;
                    profile.FrontIdPhoto = OwnerUser.FrontIdPhoto;
                    profile.BackIdPhoto = OwnerUser.BackIdPhoto;
                    profile.ArabicAddress = OwnerUser.ArabicAddress;
                    profile.ArabicFullName = OwnerUser.ArabicFullName;
                    profile.NationalIDNumber = OwnerUser.NationalIDNumber;
                    profile.FrontIdPhoto = OwnerUser.FrontIdPhoto;
                    profile.BackIdPhoto = OwnerUser.BackIdPhoto;
                    profile.Photo = OwnerUser.Photo;
                    profile.AccountStatus = OwnerUser.AccountStatus;
                }
                else
                {
                    Organizer OrganizerUser = (Organizer)user;
                    profile.Photo = OrganizerUser.Photo;
                    profile.Gender = OrganizerUser.Gender;
                    profile.Country = OrganizerUser.Country;
                    profile.BIO = OrganizerUser.BIO;
                    profile.ExperienceYear = OrganizerUser.ExperienceYear;
                    profile.Specialization = OrganizerUser.Specialization;
                    profile.FrontIdPhoto = OrganizerUser.FrontIdPhoto;
                    profile.BackIdPhoto = OrganizerUser.BackIdPhoto;
                    profile.ArabicAddress = OrganizerUser.ArabicAddress;
                    profile.ArabicFullName = OrganizerUser.ArabicFullName;
                    profile.NationalIDNumber = OrganizerUser.NationalIDNumber;
                    profile.FrontIdPhoto = OrganizerUser.FrontIdPhoto;
                    profile.BackIdPhoto = OrganizerUser.BackIdPhoto;
                    profile.Photo = OrganizerUser.Photo;
                    profile.AccountStatus = OrganizerUser.AccountStatus;
                    
                }
                return View("EditProfile", profile);
            }
            else
                { return RedirectToAction("Login", "Account"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileVM profile)
        {
            if (ModelState.IsValid) 
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _managerUser.FindByIdAsync(userId);
                if (await _managerUser.IsInRoleAsync(user, "Owner"))
                {
                    Owner OwnerUser = (Owner)user;
                    OwnerUser.Gender = profile.Gender;
                    OwnerUser.Country = profile.Country;
                    OwnerUser.BIO = profile.BIO;
                    OwnerUser.ArabicAddress = profile.ArabicAddress;
                    OwnerUser.ArabicFullName = profile.ArabicFullName;
                    OwnerUser.NationalIDNumber = profile.NationalIDNumber;
                    OwnerUser.AccountStatus = profile.AccountStatus;


                    if (profile.RemovePhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OwnerUser.Photo}");
                        OwnerUser.Photo = null;
                    }
                    else if (profile.PhotoFile != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.PhotoFile);
                        OwnerUser.Photo = $"/images/{PhotoName}";
                    }


                    if (profile.RemoveFrontIdPhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OwnerUser.FrontIdPhoto}");
                        OwnerUser.FrontIdPhoto = null;
                    }
                    else if (profile.FrontIdFile != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.FrontIdFile);
                        OwnerUser.FrontIdPhoto = $"/images/{PhotoName}";
                    }


                    if (profile.RemoveBacktIdPhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OwnerUser.BackIdPhoto}");
                        OwnerUser.BackIdPhoto = null;
                    }
                    else if (profile.BackIdFile != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.BackIdFile);
                        OwnerUser.BackIdPhoto = $"/images/{PhotoName}";
                    }
                }
                else
                {
                    Organizer OrganizerUser = (Organizer)user;
                    OrganizerUser.Photo = profile.Photo;
                    OrganizerUser.Gender = profile.Gender;
                    OrganizerUser.Country = profile.Country;
                    OrganizerUser.BIO = profile.BIO;
                    OrganizerUser.ExperienceYear = profile.ExperienceYear;
                    OrganizerUser.Specialization = profile.Specialization;
                    OrganizerUser.ArabicAddress = profile.ArabicAddress;
                    OrganizerUser.ArabicFullName = profile.ArabicFullName;
                    OrganizerUser.NationalIDNumber = profile.NationalIDNumber;
                    OrganizerUser.AccountStatus = profile.AccountStatus;

                    if (profile.RemovePhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OrganizerUser.Photo}");
                        OrganizerUser.Photo = null;
                    }
                    else if (profile.PhotoFile != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.PhotoFile);
                        OrganizerUser.Photo = $"/images/{PhotoName}";
                    }


                    if (profile.RemoveFrontIdPhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OrganizerUser.FrontIdPhoto}");
                        OrganizerUser.FrontIdPhoto = null;
                    }
                    else if (profile.FrontIdPhoto != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.FrontIdFile);
                        OrganizerUser.FrontIdPhoto = $"/images/{PhotoName}";
                    }


                    if (profile.RemoveBacktIdPhoto)
                    {
                        UploadProfilePhoto.RemoveFile($"wwwroot{OrganizerUser.BackIdPhoto}");
                        OrganizerUser.BackIdPhoto = null;
                    }
                    else if (profile.BackIdPhoto != null)
                    {
                        var PhotoName = UploadProfilePhoto.UploadFile("images", profile.BackIdFile);
                        OrganizerUser.BackIdPhoto = $"/images/{PhotoName}";
                    }
                }
                await _managerUser.UpdateAsync(user);
                return RedirectToAction("Index", "Profile");
            }
            else { return View(profile); }
        }



        public async Task<IActionResult> Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (User.IsInRole("Owner"))
                {
                    var venues = _managerVenues.GetByUserId(int.Parse(userId));
                    foreach (var venue in venues)
                    {
                        var VenueToDelete = _managerVenues.GetByIdWithIncludes(venue.Id);
                        if (VenueToDelete.Events.Any(v => v.Status == EventStatusEnum.Paid))
                        {
                            TempData["DeleteProfileError"] = true;
                            return RedirectToAction("Edit");
                        }
                    }
                    foreach (var venue in venues)
                        { var result = _managerVenues.Delete(venue.Id); }
                }
                else if (User.IsInRole("Organizer"))
                {
                    var events = _managerEvents.GetByUserId(int.Parse(userId));
                    foreach (var event_ in events)
                        { var result = _managerEvents.Delete(event_.EventId); }
                }
                await _managerUser.DeleteAsync(await _managerUser.FindByIdAsync(userId));
            }
            return RedirectToAction("Logout", "Account");
        }
    }
}
