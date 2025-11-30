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

        public int Delete(int id)
        {
            var ev = context.Events.FirstOrDefault(x => x.EventId == id);
            var paument = context.Payments.Where(x=>x.EventId == id).ToList();
            foreach (var p in paument)
            {
                p.EventId = null;
            }
            context.Events.Remove(ev);
            return context.SaveChanges();            
        }

        public List<Event> Get3()
        {
            var res = context.Events.Take(3).Include(e => e.EventPhotos).ToList();
            return res;
        }



        public List<Event> GetByFilter_Search(string? title,SortingTypeEnum sortingType,SortByEnum? sortBy,string?city, int? category, decimal? maxPrice, DateTime? startDate,DateTime? enddate, bool? isPrivate)

        {
            var query = context.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(e => e.EventTitle.Contains(title));

            if (category != null)
                query = query.Where(e => (int)e.Category == category);

            if (sortBy.HasValue)
            {
                if (sortBy == SortByEnum.PriceAscending)
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value ).OrderBy(e => e.TicketPrice);
                else
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value).OrderByDescending(e => e.TicketPrice);

            }
            if (sortBy == SortByEnum.Date)
            {
                if (sortingType == SortingTypeEnum.Ascending)
                    query = query.Where(v => v.StartDateTime <= startDate.Value && v.EndDateTime>=enddate).OrderBy(e => e.StartDateTime);
                else
                    query = query.Where(v => v.StartDateTime <= startDate.Value && v.EndDateTime >= enddate).OrderByDescending(e => e.StartDateTime);

            }
            if (sortBy.HasValue)
            {
                if (sortBy == SortByEnum.CapacityAscending)
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value).OrderBy(e => e.TicketPrice);
                else
                    query = query.Where(v => v.TicketPrice <= maxPrice.Value).OrderByDescending(e => e.TicketPrice);

            }

            if (startDate != null)
                query = query.Where(e => e.StartDateTime.Date == startDate.Value.Date);

            if (isPrivate != null)
                query = query.Where(e => e.IsPrivate == isPrivate);

            if (city!=null)
                query=query.Where(e=>e.Address==city);



            return query.ToList();
        }


        public Event GetById(int id)
        {
            var res = context.Events.FirstOrDefault(ev=>ev.EventId == id);
            if (res == null)
            {
                return null;
            }
            return res;
        }

        public List<Event> GetByUserId(int id)
        {
            return context.Events.Where(e => e.OrganizerId == id).Include(e => e.EventPhotos).ToList();
        }

        public int Insert(Event obj)
        {
            context.Events.Add(obj);
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

    }
}
