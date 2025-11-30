using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Seeding_Data
{
    public class EventPhotoSeedData : IEntityTypeConfiguration<EventPhoto>
    {
        public void Configure(EntityTypeBuilder<EventPhoto> builder)
        {
            builder.HasData(
                // Event 1
                new EventPhoto { EventId = 1, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 1, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 1, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 1, PhotoUrl = "/images/event1_4.jpg" },

                // Event 2
                new EventPhoto { EventId = 2, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 2, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 2, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 2, PhotoUrl = "/images/event1_4.jpg" },
                                 
                // Event 3
                new EventPhoto { EventId = 3, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 3, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 3, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 3, PhotoUrl = "/images/event1_4.jpg" },

                // Event 4
                new EventPhoto { EventId = 4, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 4, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 4, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 4, PhotoUrl = "/images/event1_4.jpg" },

                // Event 5
                new EventPhoto { EventId = 5, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 5, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 5, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 5, PhotoUrl = "/images/event1_4.jpg" },

                // Event 6
                new EventPhoto { EventId = 6, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 6, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 6, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 6, PhotoUrl = "/images/event1_4.jpg" },

                // Event 7
                new EventPhoto { EventId = 7, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 7, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 7, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 7, PhotoUrl = "/images/event1_4.jpg" },

                // Event 8
                new EventPhoto { EventId = 8, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 8, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 8, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 8, PhotoUrl = "/images/event1_4.jpg" },

                // Event 9
                new EventPhoto { EventId = 9, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 9, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 9, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 9, PhotoUrl = "/images/event1_4.jpg" },

                // Event 10
                new EventPhoto { EventId = 10, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 10, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 10, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 10, PhotoUrl = "/images/event1_4.jpg" },

                // Event 11
                new EventPhoto { EventId = 11, PhotoUrl = "/images/event1_1.jpg" },
                new EventPhoto { EventId = 11, PhotoUrl = "/images/event1_2.jpg" },
                new EventPhoto { EventId = 11, PhotoUrl = "/images/event1_3.jpg" },
                new EventPhoto { EventId = 11, PhotoUrl = "/images/event1_4.jpg" }
            );

        }
    }
}
