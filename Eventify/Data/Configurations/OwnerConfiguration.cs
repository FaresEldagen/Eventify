using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            builder.HasKey(o => o.OwnerId);

            builder.Property(o => o.UserName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.Password)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            builder.Property(o => o.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(o => o.Photo)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(false);

            builder.Property(o => o.Gander)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(o => o.CellPhone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(o => o.BIO)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(false);

            builder.Property(o => o.Country)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            builder.Property(o => o.VenueCount)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(o => o.WithdrawableEarnings)
                .HasColumnType("DECIMAL")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.JoinedDate)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(o => o.FrontIDPhoto)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(false);

            builder.Property(o => o.BackIDPhoto)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(false);

            builder.Property(o => o.ArabicFullName)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            builder.Property(o => o.ArabicAddress)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            //NavigationProperty
            builder.HasMany(o => o.Venues).WithOne(s => s.Owner).HasForeignKey(s => s.OwnerId);
        }
    }
}
