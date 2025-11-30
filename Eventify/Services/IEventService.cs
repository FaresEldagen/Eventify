using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.Services
{
    public interface IEventService : IGenericService<Event>
    {
        public List<Event> GetByFilter_Search(
            string? title,
            SortByEnum? sortBy,
            int pageNumber,
            EventCategoryEnum? category,
            decimal? maxPrice,
            DateTime? startDate,
            DateTime? endDate,
            bool? isPrivate,
            out int totalEvents);

    }
}
