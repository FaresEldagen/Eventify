using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Eventify.Models.Entities;
using Eventify.Models.Enums;

public class OwnerSeedData : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        var hasher = new PasswordHasher<Owner>();

        var Owner1 = new Owner
        {
            Id = 1,
            UserName = "Mahmoud",
            NormalizedUserName = "MAHMOUD",
            Email = "Owner1@test.com",
            NormalizedEmail = "OWNER1@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            JoinedDate = new DateTime(2021, 1, 10),
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Owner1.PasswordHash = hasher.HashPassword(Owner1, "Owner123!");

        var Owner2 = new Owner
        {
            Id = 2,
            UserName = "Ali",
            NormalizedUserName = "ALI",
            Email = "Owner2@test.com",
            NormalizedEmail = "OWNER2@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            JoinedDate = new DateTime(2021, 1, 10),
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Owner2.PasswordHash = hasher.HashPassword(Owner2, "Owner123!");

        var Owner3 = new Owner
        {
            Id = 3,
            UserName = "Amr",
            NormalizedUserName = "AMR",
            Email = "Owner3@test.com",
            NormalizedEmail = "OWNER3@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            JoinedDate = new DateTime(2021, 1, 10),
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Owner3.PasswordHash = hasher.HashPassword(Owner3, "Owner123!");

        var Owner4 = new Owner
        {
            Id = 11,
            UserName = "Rami",
            NormalizedUserName = "RAMI",
            Email = "Owner4@test.com",
            NormalizedEmail = "OWNER4@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            JoinedDate = new DateTime(2021, 1, 10),
            NationalIDNumber = "29701020123455",
            VenueCount = 0,
            WithdrawableEarnings = 15000.75m,
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Pending
        };
        Owner4.PasswordHash = hasher.HashPassword(Owner4, "Owner123!");

        var Owner5 = new Owner
        {
            Id = 12,
            UserName = "Anas",
            NormalizedUserName = "ANAS",
            Email = "Owner5@test.com",
            NormalizedEmail = "OWNER5@TEST.COM",
            EmailConfirmed = true,
            Photo = "/Images/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.SaudiArabia,
            FrontIdPhoto = "/Images/front1.jpg",
            BackIdPhoto = "/Images/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            JoinedDate = new DateTime(2021, 1, 10),
            NationalIDNumber = "29701020123455",
            VenueCount = 0,
            WithdrawableEarnings = 15000.75m,
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Declined
        };
        Owner5.PasswordHash = hasher.HashPassword(Owner5, "Owner123!");

        builder.HasData(Owner1, Owner2, Owner3, Owner4, Owner5);
    }
}
