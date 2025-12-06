using Eventify.Models.Entities;

namespace Eventify.ViewModels.AdminVm
{
    public class AdminDashboardVM
    {
        public List<Admin> Admins = new List<Admin>();
        public List<ApplicationUser> Users = new List<ApplicationUser>();
        public List<Venue> Venues = new List<Venue>();
        public List<Event> Events = new List<Event>();
        public List<Payment> Payments = new List<Payment>();
    }
}
