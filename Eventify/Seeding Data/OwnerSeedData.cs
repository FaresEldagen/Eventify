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

        // Password = Owner123!
        var Owner1 = new Owner
        {
            Id = 1,
            UserName = "Mahmoud",
            NormalizedUserName = "MAHMOUD",
            Email = "Owner1@test.com",
            NormalizedEmail = "OWNER1@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            PasswordHash = "AQAAAAIAAYagAAAAEJDR5e/KRGRvWdv3r0tq7vBzbc5mMQkdHkvRT6ImbqRUtqvGfU9CgRpTnMlZzGpu6g=="
        };

        var Owner2 = new Owner
        {
            Id = 2,
            UserName = "Ali",
            NormalizedUserName = "ALI",
            Email = "Owner2@test.com",
            NormalizedEmail = "OWNER2@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            PasswordHash = "AQAAAAIAAYagAAAAECWLo9pP7IpTIXFZf3p1gC/oZ3S0dG1kbYv2HNX3zTGai6U5bD4Jv0FYpkuNeLbUzA=="
        };

        var Owner3 = new Owner
        {
            Id = 3,
            UserName = "Amr",
            NormalizedUserName = "AMR",
            Email = "Owner3@test.com",
            NormalizedEmail = "OWNER3@TEST.COM",
            EmailConfirmed = true,
            Photo = "~/image/avatar.jpg",
            Gender = GenderEnum.Male,
            BIO = "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.",
            Country = CountryEnum.Egypt,
            FrontIdPhoto = "~/image/front1.jpg",
            BackIdPhoto = "~/image/back1.jpg",
            ArabicAddress = "القاهرة - مصر",
            ArabicFullName = "محمود سمير عبد الله",
            NationalIDNumber = "29701020123455",
            VenueCount = 3,
            WithdrawableEarnings = 15000.75m,
            PasswordHash = "AQAAAAIAAYagAAAAECrYsbscgfNyV2pIB3BdYoCHf0UMxJVUZ0TBDFIJx+xxii2kxMvWZHDfL/pWc8Gzgw=="
        };

        builder.HasData(Owner1, Owner2, Owner3);
    }
}
