using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.ToTable("Venues");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150).IsRequired();

            builder.Property(v => v.VenueType)
                .HasConversion<int>().IsRequired();

            builder.Property(v => v.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250).IsRequired();

            builder.Property(v => v.Country)
                .HasConversion<int>().IsRequired();

            builder.Property(v => v.ZIP)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20).IsRequired();

            builder.Property(v => v.Description)
                .HasColumnType("VARCHAR(Max)").IsRequired();

            builder.Property(v => v.Capacity)
                .HasColumnType("INT").IsRequired();

            builder.Property(v => v.PricePerHour)
                .HasColumnType("INT").IsRequired();

            builder.Property(v => v.SpecialFeatures)
                .HasColumnType("VARCHAR(Max)").IsRequired();

            builder.Property(v => v.AirConditioningAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.CateringAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.WifiAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.ParkingAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.BarServiceAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.RestroomsAvailable)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.AudioVisualEquipment)
                .HasColumnType("BIT").IsRequired();

            builder.Property(v => v.ProofOfOwnership)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250).IsRequired();


            // Foreign-Keys
            builder.Property(v => v.OwnerId)
                .HasColumnType("INT").IsRequired();

            // Relationships
            // Owners with Venues
            builder.HasOne(v => v.Owner)
                .WithMany(own => own.Venues)
                .HasForeignKey(v => v.OwnerId)
                .IsRequired();

            // Venue Photos
            builder.HasMany(e => e.VenuePhotos)
                .WithOne(ph => ph.Venue)
                .HasForeignKey(e => e.VenueId)
                .IsRequired();
        }
    }
}
