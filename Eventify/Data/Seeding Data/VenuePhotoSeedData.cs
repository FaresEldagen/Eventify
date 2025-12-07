using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Seeding_Data
{
    public class VenuePhotoSeedData : IEntityTypeConfiguration<VenuePhoto>
    {
        public void Configure(EntityTypeBuilder<VenuePhoto> builder)
        {
            builder.HasData(
                // Venue 1
                new VenuePhoto {VenueId = 1, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 2
                new VenuePhoto { VenueId = 2, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 3
                new VenuePhoto { VenueId = 3, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 4
                new VenuePhoto { VenueId = 4, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 5
                new VenuePhoto { VenueId = 5, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 6
                new VenuePhoto { VenueId = 6, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 7
                new VenuePhoto { VenueId = 7, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 8
                new VenuePhoto {VenueId = 8, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 9
                new VenuePhoto {VenueId = 9, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 10
                new VenuePhoto {VenueId = 10, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 11
                new VenuePhoto { VenueId = 11, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 11, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 11, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 11, PhotoUrl = "/images/venue1_4.jpg" },

                // Venue 12
                new VenuePhoto { VenueId = 12, PhotoUrl = "/images/venue1_1.jpg" },
                new VenuePhoto { VenueId = 12, PhotoUrl = "/images/venue1_2.jpg" },
                new VenuePhoto { VenueId = 12, PhotoUrl = "/images/venue1_3.jpg" },
                new VenuePhoto { VenueId = 12, PhotoUrl = "/images/venue1_4.jpg" }
            );
        }
    }
}
