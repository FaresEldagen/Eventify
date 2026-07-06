using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Eventify.Models.Enums;
using Eventify.Models.Entities;

public class AdminSeedData : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        var hasher = new PasswordHasher<Admin>();

        var Admin1 = new Admin
        {
            Id = 7,
            UserName = "Mohamed",
            NormalizedUserName = "MOHAMED",
            Email = "Admin1@test.com",
            NormalizedEmail = "ADMIN1@TEST.COM",
            EmailConfirmed = true,
            JoinedDate = new DateTime(2022, 1, 15),
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Admin1.PasswordHash = hasher.HashPassword(Admin1, "Admin123!");

        var Admin2 = new Admin
        {
            Id = 8,
            UserName = "Aabas",
            NormalizedUserName = "AABAS",
            Email = "Admin2@test.com",
            NormalizedEmail = "ADMIN2@TEST.COM",
            EmailConfirmed = true,
            JoinedDate = new DateTime(2022, 1, 15),
            SecurityStamp = "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54",
            AccountStatus = AccountStatus.Verified
        };
        Admin2.PasswordHash = hasher.HashPassword(Admin2, "Admin123!");

        builder.HasData(Admin1, Admin2);
    }
}
