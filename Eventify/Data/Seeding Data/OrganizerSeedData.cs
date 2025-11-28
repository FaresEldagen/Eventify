using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Eventify.Models.Enums;
using Eventify.Models.Entities;

public class OrganizerSeedData : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        var hasher = new PasswordHasher<Organizer>();

        var Oraganizer1 = new Organizer
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
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54"
        };
        Oraganizer1.PasswordHash = hasher.HashPassword(Oraganizer1, "Organizer123!");

        var Oraganizer2 = new Organizer
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
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54"
        };
        Oraganizer2.PasswordHash = hasher.HashPassword(Oraganizer2, "Organizer123!");

        var Oraganizer3 = new Organizer
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
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54"
        };
        Oraganizer3.PasswordHash = hasher.HashPassword(Oraganizer3, "Organizer123!");

        builder.HasData(Oraganizer1, Oraganizer2, Oraganizer3);
    }
}
