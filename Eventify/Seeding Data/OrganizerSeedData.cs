using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Eventify.Models.Enums;
using Eventify.Models.Entities;

public class OrganizerSeedData : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        // Password = Organizer123!

        var Organizer1 = new Organizer
        {
            Id = 4,
            UserName = "Fares",
            NormalizedUserName = "FARES",
            Email = "Organizer1@test.com",
            NormalizedEmail = "ORGANIZER1@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = DateTime.SpecifyKind(new DateTime(2022, 1, 15), DateTimeKind.Utc),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            PasswordHash = "AQAAAAIAAYagAAAAEJBpTyyvJ0sHYPQ+NgQe5y3n2a1Zgxk2c9RuyHo7aUZPuBfVUY41k2K5HkTtbcX6Rw=="
        };

        var Organizer2 = new Organizer
        {
            Id = 5,
            UserName = "Ahmed",
            NormalizedUserName = "AHMED",
            Email = "Organizer2@test.com",
            NormalizedEmail = "ORGANIZER2@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = DateTime.SpecifyKind(new DateTime(2022, 1, 15), DateTimeKind.Utc),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            PasswordHash = "AQAAAAIAAYagAAAAEH5RBCYP6r/JwuUGaSrt+i/xwcA3AQ8UDxRhq4HYBIN1vRIjguQK6pVVZ9Ge6xGdDQ=="
        };

        var Organizer3 = new Organizer
        {
            Id = 6,
            UserName = "Ziad",
            NormalizedUserName = "ZIAD",
            Email = "Organizer3@test.com",
            NormalizedEmail = "ORGANIZER3@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = DateTime.SpecifyKind(new DateTime(2022, 1, 15), DateTimeKind.Utc),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            PasswordHash = "AQAAAAIAAYagAAAAEOQsgVdVfIpBwUuFRGflz4CfyxTIDJFC9wDziNk5DPFnKwCXH1d0M0mU7WLS0VPlSw=="
        };

        builder.HasData(Organizer1, Organizer2, Organizer3);
    }
}