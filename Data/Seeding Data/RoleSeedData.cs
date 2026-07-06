using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class RoleSeedData : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.HasData(
            new IdentityRole<int> { Id = 1, Name = "Owner", NormalizedName = "OWNER" },
            new IdentityRole<int> { Id = 2, Name = "Organizer", NormalizedName = "ORGANIZER" },
            new IdentityRole<int> { Id = 3, Name = "Admin", NormalizedName = "ADMIN" }
        );
    }
}
