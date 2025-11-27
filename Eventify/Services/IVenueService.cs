using Eventify.Models.Entities;

namespace Eventify.Services
{
    public interface IVenueService : IGenericService<Venue>
    {
        public List<Venue> GetByOwnerId(int id);
    }
}
