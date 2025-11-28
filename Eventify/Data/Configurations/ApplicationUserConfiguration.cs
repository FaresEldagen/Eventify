using Eventify.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasDiscriminator<string>("UserType")
                .HasValue<Organizer>("Organizier").HasValue<Owner>("Owner");



            builder.Property(e => e.UserName)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired();

            builder.Property(e => e.Photo)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);

            builder.Property(e => e.Gender)
                .HasConversion<int>().IsRequired(false);

            builder.Property(e => e.BIO)
                .HasColumnType("NVARCHAR(MAX)").IsRequired(false);

            builder.Property(e => e.Country)
                .HasConversion<int>().IsRequired(false);

            builder.Property(e => e.NationalIDNumber)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);


            builder.Property(e => e.FrontIdPhoto)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);

            builder.Property(e => e.BackIdPhoto)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(false);

            builder.Property(e => e.ArabicAddress)
                .HasColumnType("NVARCHAR").HasMaxLength(255).IsRequired(false);

            builder.Property(e => e.ArabicFullName)
                .HasColumnType("NVARCHAR").HasMaxLength(100).IsRequired(false);

            builder.Property(e => e.JoinedDate)
                .HasColumnType("DATETIME2")
                .IsRequired()
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
