using System.Globalization;
using Eventify.Data;
using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Managers
{
    public class EventManager : IEventService
    {
        AppDbContext context;
        public EventManager(AppDbContext context)
        {
            this.context = context;
        }



        public List<Event> GetByFilter_Search(
            string? title,
            SortByEnum? sortBy,
            int pageNumber,
            EventCategoryEnum? category, 
            decimal? maxPrice, 
            DateTime? startDate,
            DateTime? endDate,
            out int totalEvents)

        {
            var query = context.Events.AsQueryable();

            query = query.Where(e => e.IsPrivate == false && e.Status == EventStatusEnum.Paid);

            if (maxPrice.HasValue)
            {
                query = query.Where(v => v.TicketPrice <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(e => EF.Functions.Like(e.EventTitle,$"%{title}%") || EF.Functions.Like(e.Address, $"%{title}%"));

            if (category != null)
                query = query.Where(e => e.Category == category);

            if (sortBy.HasValue)
            {
                if (sortBy == SortByEnum.PriceAscending)
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value ).OrderBy(e => e.TicketPrice);
                else if(sortBy == SortByEnum.PriceDescending)
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value).OrderByDescending(e => e.TicketPrice);

                if (sortBy == SortByEnum.DateAscending)
                    query = query.Where(v => v.StartDateTime <= startDate.Value && v.EndDateTime >= endDate).OrderBy(e => e.StartDateTime);
                else if(sortBy == SortByEnum.DateAscending)
                    query = query.Where(v => v.StartDateTime <= startDate.Value && v.EndDateTime >= endDate).OrderByDescending(e => e.StartDateTime);

            }

            if (startDate != null)
                query = query.Where(e => e.StartDateTime.Date == startDate.Value.Date);

            


            // get related photos
            query = query.Include(v => v.EventPhotos);

            totalEvents = GetTotalEventFromQuery(query);

            int pageSize = 9;
            int skip = (pageNumber - 1) * pageSize;

            var result =query
                .Skip(skip)
                .Take(pageSize)
                .ToList();


            return result;
        }

        private int GetTotalEventFromQuery(IQueryable<Event> query)
        {
            return query.Count();
        }



        public List<Event> Get3()
        {
            var res = context.Events.Take(3).Include(e => e.EventPhotos).ToList();
            return res;
        }

        public Event GetById(int id)
        {
            var res = context.Events.FirstOrDefault(ev => ev.EventId == id);
            return res;
        }

        public Event? GetByIdWithIncludes(int id)
        {
            var res = context.Events.Include(x=>x.EventPhotos).Include(x=>x.Organizer).Include(x => x.Venue).FirstOrDefault(ev => ev.EventId == id);
            return res;
        }

        public List<Event> GetByUserId(int id)
        {
            return context.Events.Where(e => e.OrganizerId == id).Include(e => e.EventPhotos).ToList();
        }



        public int Insert(Event ev)
        {
            var Venue = context.Venues.FirstOrDefault(o => o.Id == ev.VenueId);
            ev.Address = $"{Venue.Country} / {Venue.Address}";
            ev.Capacity = Venue.Capacity;

            context.Events.Add(ev);
            return context.SaveChanges();
        }



        public int Update(Event updatedEvent)
        {
            var existingEvent = context.Events.FirstOrDefault(e => e.EventId == updatedEvent.EventId);

            if (existingEvent == null)
                return 0;

            //Normal Properties
            existingEvent.EventTitle = updatedEvent.EventTitle;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.IsPrivate = updatedEvent.IsPrivate;
            existingEvent.StartDateTime = updatedEvent.StartDateTime;
            existingEvent.EndDateTime = updatedEvent.EndDateTime;
            existingEvent.Features = updatedEvent.Features;
            existingEvent.Category = updatedEvent.Category;
            existingEvent.Status = updatedEvent.Status;
            existingEvent.TicketPrice = updatedEvent.TicketPrice;
            existingEvent.Address = updatedEvent.Address;

            // Navigation properties
            existingEvent.VenueId = updatedEvent.VenueId;
            existingEvent.OrganizerId = updatedEvent.OrganizerId;
            existingEvent.EventPhotos = updatedEvent.EventPhotos;

            return context.SaveChanges();
        }



        public int Delete(int id)
        {
            var ev = context.Events.FirstOrDefault(x => x.EventId == id);
            var paument = context.Payments.Where(x => x.EventId == id).ToList();
            foreach (var p in paument)
            {
                p.EventId = null;
            }
            context.Events.Remove(ev);
            return context.SaveChanges();
        }

        public List<Event> GetPendingEvents()
        {
            return context.Events.Where(e => e.Status == EventStatusEnum.Pending).OrderBy(e=>e.StartDateTime).ToList();
        }
    }
}
