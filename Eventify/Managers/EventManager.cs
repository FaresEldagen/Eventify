using Eventify.Data;
using Eventify.Models.Entities;
using Eventify.Services;

namespace Eventify.Managers
{
    public class EventManager : IEventService
    {
        AppDbContext context;
        public EventManager(AppDbContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Event> Get3()
        {
            throw new NotImplementedException();
        }

        public List<Event> GetByFilter_Search()
        {
            throw new NotImplementedException();
        }

        public Event GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Event GetByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Event obj)
        {
            throw new NotImplementedException();
        }

        public int Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
