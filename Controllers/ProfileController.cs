using Eventify.Managers;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Eventify.ViewModels.EventVM;
using Eventify.ViewModels.ProfileVM;
using Eventify.ViewModels.VenueVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
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
        public static string UploadFile(string webRootPath, string FolderName, IFormFile File)
        {
            try
            {
                string FolderPath = Path.Combine(webRootPath, FolderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
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
                throw;
            }

        }
        public static string RemoveFile(string webRootPath, string FileName)
        {
            try
            {
                if (FileName.StartsWith("wwwroot"))
                {
                    FileName = FileName.Substring("wwwroot".Length);
                }
                FileName = FileName.TrimStart('/', '\\');
                var directory = Path.Combine(webRootPath, FileName);

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
                throw;
            }
        }

    }

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IVenueService _managerVenues;
        private readonly IEventService _managerEvents;
        private readonly UserManager<ApplicationUser> _managerUser;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IVenueService managerVenues, IEventService managerEvents, UserManager<ApplicationUser> managerUser, IWebHostEnvironment webHostEnvironment)
        {
            _managerVenues = managerVenues;
            _managerEvents = managerEvents;
            _managerUser = managerUser;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                profile.VenueCount = user.VenueCount;
                profile.Venues = venuesCards;
                profile.WithdrawableEarnings = user.WithdrawableEarnings;
                profile.Verfication = user.AccountStatus.ToString();
                return View("PersonalVenueOwner", profile);
            }
            else
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
                profile.EventsCount = user.PastEventCount;
                profile.Events = EventCards;
                profile.Verfication = user.AccountStatus.ToString();
                return View("PersonalEventOrganizer", profile);
            }
        }

        public async Task<IActionResult> View(int id)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            if (int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) == id)
            {
                return RedirectToAction("Index");
            }

            var user = await _managerUser.FindByIdAsync(id.ToString());
            if (await _managerUser.IsInRoleAsync(user, "Owner"))
            {
                Owner OwnerUser = (Owner)user;
                List<Venue> venues = _managerVenues.GetByUserId(id);
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
                profile.Id = id;
                profile.Name = OwnerUser.UserName;
                profile.Email = OwnerUser.Email;
                profile.JoinedDate = OwnerUser.JoinedDate.ToShortDateString();
                profile.Gender = string.IsNullOrWhiteSpace(OwnerUser.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                profile.BIO = OwnerUser.BIO;
                profile.Photo = OwnerUser.Photo;
                profile.Country = string.IsNullOrWhiteSpace(OwnerUser.Country.ToString()) ? "Undefined" : user.Country.ToString();
                profile.VenueCount = OwnerUser.VenueCount;
                profile.Venues = venuesCards;
                profile.WithdrawableEarnings = OwnerUser.WithdrawableEarnings;
                return View("OtherVenueOwner", profile);
            }
            else if (await _managerUser.IsInRoleAsync(user, "Organizer"))
            {
                Organizer OrganizerUser = (Organizer)user;
                List<Event> events = _managerEvents.GetByUserId(id);
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
                profile.Id = id;
                profile.Name = OrganizerUser.UserName;
                profile.Email = OrganizerUser.Email;
                profile.JoinedDate = OrganizerUser.JoinedDate.ToShortDateString();
                profile.Gender = string.IsNullOrWhiteSpace(OrganizerUser.Gender.ToString()) ? "Undefined" : user.Gender.ToString();
                profile.BIO = OrganizerUser.BIO;
                profile.Photo = OrganizerUser.Photo;
                profile.Country = string.IsNullOrWhiteSpace(OrganizerUser.Country.ToString()) ? "Undefined" : user.Country.ToString();
                profile.Specialization = OrganizerUser.Specialization ?? "Undefined";
                profile.ExperienceYear = OrganizerUser.ExperienceYear;
                profile.EventsCount = OrganizerUser.PastEventCount;
                profile.Events = EventCards;
                return View("OtherEventOrganizer", profile);
            }
            else
                return NotFound();
        }



        public async Task<IActionResult> Edit()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                profile.Phone = OwnerUser.PhoneNumber;
            }
            else if (await _managerUser.IsInRoleAsync(user, "Organizer"))
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
                profile.Phone = OrganizerUser.PhoneNumber;
                
            }
            return View("EditProfile", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileVM profile)
        {
            if (ModelState.IsValid) 
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _managerUser.FindByIdAsync(userId);
                try
                {
                    if (await _managerUser.IsInRoleAsync(user, "Owner"))
                    {
                        Owner OwnerUser = (Owner)user;
                        OwnerUser.Gender = profile.Gender;
                        OwnerUser.Country = profile.Country;
                        OwnerUser.BIO = profile.BIO;
                        OwnerUser.ArabicAddress = profile.ArabicAddress;
                        OwnerUser.ArabicFullName = profile.ArabicFullName;
                        OwnerUser.NationalIDNumber = profile.NationalIDNumber;
                        OwnerUser.AccountStatus = AccountStatus.Pending;
                        OwnerUser.PhoneNumber = profile.Phone;


                        if (profile.RemovePhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OwnerUser.Photo}"); } catch {}
                            OwnerUser.Photo = null;
                        }
                        else if (profile.PhotoFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.PhotoFile);
                            OwnerUser.Photo = $"/images/{PhotoName}";
                        }


                        if (profile.RemoveFrontIdPhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OwnerUser.FrontIdPhoto}"); } catch {}
                            OwnerUser.FrontIdPhoto = null;
                        }
                        else if (profile.FrontIdFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.FrontIdFile);
                            OwnerUser.FrontIdPhoto = $"/images/{PhotoName}";
                        }


                        if (profile.RemoveBackIdPhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OwnerUser.BackIdPhoto}"); } catch {}
                            OwnerUser.BackIdPhoto = null;
                        }
                        else if (profile.BackIdFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.BackIdFile);
                            OwnerUser.BackIdPhoto = $"/images/{PhotoName}";
                        }
                    }
                    else
                    {
                        Organizer OrganizerUser = (Organizer)user;
                        OrganizerUser.Gender = profile.Gender;
                        OrganizerUser.Country = profile.Country;
                        OrganizerUser.BIO = profile.BIO;
                        OrganizerUser.ExperienceYear = profile.ExperienceYear;
                        OrganizerUser.Specialization = profile.Specialization;
                        OrganizerUser.ArabicAddress = profile.ArabicAddress;
                        OrganizerUser.ArabicFullName = profile.ArabicFullName;
                        OrganizerUser.NationalIDNumber = profile.NationalIDNumber;
                        OrganizerUser.AccountStatus = AccountStatus.Pending;
                        OrganizerUser.PhoneNumber = profile.Phone;


                        if (profile.RemovePhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OrganizerUser.Photo}"); } catch {}
                            OrganizerUser.Photo = null;
                        }
                        else if (profile.PhotoFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.PhotoFile);
                            OrganizerUser.Photo = $"/images/{PhotoName}";
                        }


                        if (profile.RemoveFrontIdPhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OrganizerUser.FrontIdPhoto}"); } catch {}
                            OrganizerUser.FrontIdPhoto = null;
                        }
                        else if (profile.FrontIdFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.FrontIdFile);
                            OrganizerUser.FrontIdPhoto = $"/images/{PhotoName}";
                        }


                        if (profile.RemoveBackIdPhoto)
                        {
                            try { UploadProfilePhoto.RemoveFile(_webHostEnvironment.WebRootPath, $"wwwroot{OrganizerUser.BackIdPhoto}"); } catch {}
                            OrganizerUser.BackIdPhoto = null;
                        }
                        else if (profile.BackIdFile != null)
                        {
                            var PhotoName = UploadProfilePhoto.UploadFile(_webHostEnvironment.WebRootPath, "images", profile.BackIdFile);
                            OrganizerUser.BackIdPhoto = $"/images/{PhotoName}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error uploading/saving profile photos: " + ex.Message);
                }

                if (ModelState.IsValid)
                {
                    await _managerUser.UpdateAsync(user);
                    return RedirectToAction("Index", "Profile");
                }
            }
            return View(profile);
        }



        public async Task<IActionResult> Delete()
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
                foreach (var evnt in events)
                {
                    if (evnt.Status == EventStatusEnum.Paid)
                    {
                        TempData["DeleteProfileError"] = true;
                        return RedirectToAction("Edit");
                    }
                }
                foreach (var event_ in events)
                    { var result = _managerEvents.Delete(event_.EventId); }
            }
            await _managerUser.DeleteAsync(await _managerUser.FindByIdAsync(userId));
            return RedirectToAction("login", "Account");
        }
    }
}
