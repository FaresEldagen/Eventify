using Eventify.Models.Enums;

namespace Eventify.ViewModels.EventVM
{
    public class EventBrowseViewModel
    {
        public string? Title { get; set; }
        public SortByEnum? SortBy {get;set;}
        public EventCategoryEnum? Category {get;set;}
        public decimal? MaxPrice {get;set;}
        public DateTime? StartDate {get;set;}
        public DateTime? EndDate {get;set;}
        public bool? IsPrivate {get;set;}

        public int PageNumber { get; set; }
        public List<EventCardVM> EventCards { get; set; } = new List<EventCardVM>();
        public int TotalPages { get; set; }

        public EventBrowseViewModel()
        {
            Title = null;
            SortBy = null;
            Category = null;
            MaxPrice = 500;
            StartDate = null;
            EndDate = null;
            IsPrivate = null;
            PageNumber = 1;
            EventCards = new List<EventCardVM>();
            TotalPages = 1;
        }

    }
}
