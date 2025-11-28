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
                new VenuePhoto {VenueId = 1, PhotoUrl = "~/images/venue1_1.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "~/images/venue1_2.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "~/images/venue1_3.jpg" },
                new VenuePhoto {VenueId = 1, PhotoUrl = "~/images/venue1_4.jpg" },

                // Venue 2
                new VenuePhoto { VenueId = 2, PhotoUrl = "~/images/venue2_1.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "~/images/venue2_2.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "~/images/venue2_3.jpg" },
                new VenuePhoto { VenueId = 2, PhotoUrl = "~/images/venue2_4.jpg" },

                // Venue 3
                new VenuePhoto { VenueId = 3, PhotoUrl = "~/images/venue3_1.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "~/images/venue3_2.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "~/images/venue3_3.jpg" },
                new VenuePhoto { VenueId = 3, PhotoUrl = "~/images/venue3_4.jpg" },

                // Venue 4
                new VenuePhoto { VenueId = 4, PhotoUrl = "~/images/venue4_1.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "~/images/venue4_2.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "~/images/venue4_3.jpg" },
                new VenuePhoto { VenueId = 4, PhotoUrl = "~/images/venue4_4.jpg" },

                // Venue 5
                new VenuePhoto { VenueId = 5, PhotoUrl = "~/images/venue5_1.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "~/images/venue5_2.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "~/images/venue5_3.jpg" },
                new VenuePhoto { VenueId = 5, PhotoUrl = "~/images/venue5_4.jpg" },

                // Venue 6
                new VenuePhoto { VenueId = 6, PhotoUrl = "~/images/venue6_1.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "~/images/venue6_2.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "~/images/venue6_3.jpg" },
                new VenuePhoto { VenueId = 6, PhotoUrl = "~/images/venue6_4.jpg" },

                // Venue 7
                new VenuePhoto { VenueId = 7, PhotoUrl = "~/images/venue7_1.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "~/images/venue7_2.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "~/images/venue7_3.jpg" },
                new VenuePhoto { VenueId = 7, PhotoUrl = "~/images/venue7_4.jpg" },

                // Venue 8
                new VenuePhoto {VenueId = 8, PhotoUrl = "~/images/venue8_1.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "~/images/venue8_2.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "~/images/venue8_3.jpg" },
                new VenuePhoto {VenueId = 8, PhotoUrl = "~/images/venue8_4.jpg" },

                // Venue 9
                new VenuePhoto {VenueId = 9, PhotoUrl = "~/images/venue9_1.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "~/images/venue9_2.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "~/images/venue9_3.jpg" },
                new VenuePhoto {VenueId = 9, PhotoUrl = "~/images/venue9_4.jpg" },

                // Venue 10
                new VenuePhoto {VenueId = 10, PhotoUrl = "~/images/venue10_1.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "~/images/venue10_2.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "~/images/venue10_3.jpg" },
                new VenuePhoto {VenueId = 10, PhotoUrl = "~/images/venue10_4.jpg" }
            );
        }
    }
}
