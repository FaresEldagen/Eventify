using Microsoft.EntityFrameworkCore;
using Eventify.Models.Entities;
using Eventify.Services;
using Eventify.Data;

namespace Eventify.Managers
{
    public class VenueManager : IVenueService
    {
        AppDbContext context;
        public VenueManager(AppDbContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Venue> Get3()
        {
            throw new NotImplementedException();
        }

        public List<Venue> GetByFilter_Search()
        {
            throw new NotImplementedException();
        }

        public Venue GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Venue GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Venue obj)
        {
            throw new NotImplementedException();
        }

        public int Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
