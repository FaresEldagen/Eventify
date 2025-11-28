using Eventify.Models.Entities;
using Eventify.Models.Enums;

namespace Eventify.Services
{
    public interface IEventService : IGenericService<Event>
    {
        public List<Event> GetByFilter_Search(string? title, SortingTypeEnum sortingType, SortByEnum sortBy, string? city, int? category, decimal? maxPrice, DateTime? startDate, DateTime? enddate, bool? isPrivate);

    }
}
