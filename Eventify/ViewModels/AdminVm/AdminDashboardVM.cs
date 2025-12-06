using Eventify.Models.Dtos;
using Eventify.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels.AdminVm
{
    public class AdminDashboardVM
    {
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [Required(ErrorMessage ="The Email is Required")]
        public string? UserEmail { get; set; }
        public List<ApplicationUser>? Admins = new List<ApplicationUser>();
        public List<AdminViewUsersDto>? Users = new List<AdminViewUsersDto>();
        public List<Venue>? Venues = new List<Venue>();
        public List<Event>? Events = new List<Event>();
        public List<Payment>? Payments = new List<Payment>();
    }
}
