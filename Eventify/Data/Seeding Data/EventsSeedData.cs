using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Seeding_Data
{
    public class EventsSeedData : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(
                new Event
                {
                    EventId = 1,
                    EventTitle = "Tech Innovations Conference 2025",
                    Description = "A conference discussing the future of AI and technology.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 12, 12, 10, 0, 0),
                    EndDateTime = new DateTime(2025, 12, 12, 18, 0, 0),
                    Features = "Speakers, Workshops, Networking",
                    Category = EventCategoryEnum.Conference,
                    Status = EventStatusEnum.Approved,
                    TicketPrice = 500m,
                    Address = "Cairo Grand Hall, Nasr City",
                    VenueId = 1,
                    OrganizerId = 4,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 2,
                    EventTitle = "Outdoor Fitness Bootcamp",
                    Description = "Intense fitness session with professional trainers.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 5, 3, 7, 0, 0),
                    EndDateTime = new DateTime(2025, 5, 3, 11, 0, 0),
                    Features = "Trainers, Fresh Air, Group Activities",
                    Category = EventCategoryEnum.SportsEvent,
                    Status = EventStatusEnum.Pending,
                    TicketPrice = 150m,
                    Address = "Dubai Outdoor Arena, Marina Walk",
                    VenueId = 2,
                    OrganizerId = 5,
                    Capacity = 700,
                    EventVerification = EventVerification.Pending
                },
                new Event
                {
                    EventId = 3,
                    EventTitle = "Riyadh Business Networking Night",
                    Description = "Connect with entrepreneurs and business owners.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 6, 18, 19, 0, 0),
                    EndDateTime = new DateTime(2025, 6, 18, 23, 0, 0),
                    Features = "Networking, Snacks, Business Talks",
                    Category = EventCategoryEnum.Networking,
                    Status = EventStatusEnum.Finished,
                    TicketPrice = 300m,
                    Address = "Riyadh Event Center",
                    VenueId = 3,
                    OrganizerId = 6,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 4,
                    EventTitle = "Alexandria Summer Music Festival",
                    Description = "Live music performances by local bands.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 7, 10, 17, 0, 0),
                    EndDateTime = new DateTime(2025, 7, 10, 23, 0, 0),
                    Features = "Live Bands, Food Trucks, Sea View",
                    Category = EventCategoryEnum.Other,
                    Status = EventStatusEnum.Paid,
                    TicketPrice = 200m,
                    Address = "Corniche Road",
                    VenueId = 4,
                    OrganizerId = 4,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 5,
                    EventTitle = "Giza Cultural Meetup",
                    Description = "A meetup focusing on ancient Egyptian culture.",
                    IsPrivate = true,
                    StartDateTime = new DateTime(2025, 3, 5, 14, 0, 0),
                    EndDateTime = new DateTime(2025, 3, 5, 18, 0, 0),
                    Features = "Guided Tour, Cultural Talks",
                    Category = EventCategoryEnum.Meetup,
                    Status = EventStatusEnum.Rejected,
                    TicketPrice = 250m,
                    Address = "Pyramids Road",
                    VenueId = 5,
                    OrganizerId = 4,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 6,
                    EventTitle = "Abu Dhabi Luxury Expo",
                    Description = "A premium exhibition showcasing luxury brands.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 9, 1, 10, 0, 0),
                    EndDateTime = new DateTime(2025, 9, 1, 20, 0, 0),
                    Features = "Exhibitions, VIP Lounge",
                    Category = EventCategoryEnum.TradeShow,
                    Status = EventStatusEnum.Paid,
                    TicketPrice = 800m,
                    Address = "Khalifa Street",
                    VenueId = 6,
                    OrganizerId = 6,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 7,
                    EventTitle = "Jeddah Beach Charity Run",
                    Description = "A charity sports event to support children’s hospitals.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 2, 20, 6, 0, 0),
                    EndDateTime = new DateTime(2025, 2, 20, 12, 0, 0),
                    Features = "Medals, Refreshments",
                    Category = EventCategoryEnum.CharityEvent,
                    Status = EventStatusEnum.Paid,
                    TicketPrice = 100m,
                    Address = "Jeddah North Corniche",
                    VenueId = 7,
                    OrganizerId = 4,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 8,
                    EventTitle = "Doha Tech Workshop",
                    Description = "Hands-on workshop for beginners in software development.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 8, 15, 9, 0, 0),
                    EndDateTime = new DateTime(2025, 8, 15, 16, 0, 0),
                    Features = "Coding Session, Mentors",
                    Category = EventCategoryEnum.Workshop,
                    Status = EventStatusEnum.Paid,
                    TicketPrice = 350m,
                    Address = "Doha Convention Hall",
                    VenueId = 8,
                    OrganizerId = 5,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                },
                new Event
                {
                    EventId = 9,
                    EventTitle = "Luxor Historical Seminar",
                    Description = "A seminar discussing ancient Egyptian heritage.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 11, 2, 15, 0, 0),
                    EndDateTime = new DateTime(2025, 11, 2, 19, 0, 0),
                    Features = "Speakers, Guided Discussion",
                    Category = EventCategoryEnum.Seminar,
                    Status = EventStatusEnum.Pending,
                    TicketPrice = 180m,
                    Address = "Luxor Temple Road",
                    VenueId = 9,
                    OrganizerId = 6,
                    Capacity = 700,
                    EventVerification = EventVerification.Declined
                },
                new Event
                {
                    EventId = 10,
                    EventTitle = "Sharjah Digital Art Expo",
                    Description = "A digital art exhibition featuring creatives from the region.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 10, 10, 11, 0, 0),
                    EndDateTime = new DateTime(2025, 10, 10, 20, 0, 0),
                    Features = "Digital Art Panels, Artist Meetups",
                    Category = EventCategoryEnum.Other,
                    Status = EventStatusEnum.Paid,
                    TicketPrice = 220m,
                    Address = "Sharjah Art Gallery",
                    VenueId = 10,
                    OrganizerId = 4,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                }, 
                new Event
                {
                    EventId = 11,
                    EventTitle = "Giza Cultural Meetup",
                    Description = "A meetup focusing on ancient Egyptian culture.",
                    IsPrivate = false,
                    StartDateTime = new DateTime(2025, 3, 6, 14, 0, 0),
                    EndDateTime = new DateTime(2025, 3, 6, 18, 0, 0),
                    Features = "Guided Tour, Cultural Talks",
                    Category = EventCategoryEnum.Meetup,
                    Status = EventStatusEnum.Pending,
                    TicketPrice = 250m,
                    Address = "Pyramids Road",
                    VenueId = 5,
                    OrganizerId = 5,
                    Capacity = 700,
                    EventVerification = EventVerification.Verified
                }
            );
        }
    }
}
