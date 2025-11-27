using Eventify.Data;
using Eventify.Models.Entities;
using Eventify.Services;
using Microsoft.AspNetCore.Mvc;

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
            var res = context.Events.FirstOrDefault(x => x.EventId == id);
            if (res != null)
            {
                context.Events.Remove(res);
                return context.SaveChanges();
            }
            return 0;
            
        }

        public List<Event> Get3()
        {
            var res = context.Events.Take(3).ToList();
            return res;
        }

        public List<Event> GetByFilter_Search()
        {
            throw new NotImplementedException();
        }

        //Not Sure About this yet
        #region
        //public List<Event> GetByFilter_Search(string? title,int? category,decimal? minPrice,decimal? maxPrice,DateTime? startDate,bool? isPrivate)
        //{
        //    //عشان متعملش الكويري وتبعتها للسكوال غير مره واحده
        //    var query = context.Events.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(title))
        //        query = query.Where(e => e.EventTitle.Contains(title));

        //    if (category != null)
        //        query = query.Where(e => (int)e.Category == category);

        //    if (minPrice != null)
        //        query = query.Where(e => e.TicketPrice >= minPrice);

        //    if (maxPrice != null)
        //        query = query.Where(e => e.TicketPrice <= maxPrice);

        //    if (startDate != null)
        //        query = query.Where(e => e.StartDateTime.Date == startDate.Value.Date);

        //    if (isPrivate != null)
        //        query = query.Where(e => e.IsPrivate == isPrivate);

        //    return query.ToList();
        //}
        ////تستخدم كده
        //public IActionResult List(string? title, int? category, decimal? min, decimal? max, bool? isPrivate)
        //{
        //    var data = eventService.GetByFilter_Search(title, category, min, max, null, isPrivate);
        //    return View(data);
        //}
        #endregion

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
            return context.Events.Where(e => e.OrganizerId == id).ToList();
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
