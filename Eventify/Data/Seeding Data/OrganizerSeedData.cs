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
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
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
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
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
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 12,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Oraganizer3.PasswordHash = hasher.HashPassword(Oraganizer3, "Organizer123!");

        var Oraganizer4 = new Organizer
        {
            Id = 9,
            UserName = "Ashraf",
            NormalizedUserName = "ASHRAF",
            Email = "Organizer4@test.com",
            NormalizedEmail = "ORGANIZER4@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.Iraq,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 0,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Declined
        };
        Oraganizer4.PasswordHash = hasher.HashPassword(Oraganizer4, "Organizer123!");

        var Oraganizer5 = new Organizer
        {
            Id = 10,
            UserName = "Zaatr",
            NormalizedUserName = "ZAATR",
            Email = "Organizer5@test.com",
            NormalizedEmail = "ORGANIZER5@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.",
            Country = CountryEnum.UnitedArabEmirates,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "فارس حسن علي الداجن",
            JoinedDate = new DateTime(2022, 1, 15),
            NationalIDNumber = "29801150123456",
            ExperienceYear = 5,
            PastEventCount = 0,
            Specialization = "Tech Events",
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Pending
        };
        Oraganizer5.PasswordHash = hasher.HashPassword(Oraganizer5, "Organizer123!");

        builder.HasData(Oraganizer1, Oraganizer2, Oraganizer3, Oraganizer4, Oraganizer5);
    }
}
