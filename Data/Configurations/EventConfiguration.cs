using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventTitle)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150).IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("VARCHAR(MAX)").IsRequired();

            builder.Property(e => e.IsPrivate)
                .HasColumnType("BIT").IsRequired();

            builder.Property(e => e.StartDateTime)
                .HasColumnType("DATETIME2").IsRequired();

            builder.Property(e => e.EndDateTime)
                .HasColumnType("DATETIME2").IsRequired();

            builder.Property(e => e.Features)
                .HasColumnType("VARCHAR(MAX)").IsRequired(false);

            builder.Property(e => e.Category)
                .HasConversion<int>().IsRequired();

            builder.Property(e => e.Status)
                .HasConversion<int>().IsRequired();

            builder.Property(e => e.TicketPrice)
                .HasColumnType("DECIMAL")
                .HasPrecision(18, 2).IsRequired();

            builder.Property(v => v.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250).IsRequired();


            // Foreign-Keys
            builder.Property(e => e.VenueId)
                .HasColumnType("INT");

            builder.Property(e => e.OrganizerId)
                .HasColumnType("INT");


            // Relationships
            //Events with Organizers
            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerId)
                .IsRequired();

            // Event with Venues
            builder.HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            // EventPhotos
            builder.HasMany(e => e.EventPhotos)
                .WithOne(ph => ph.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

        }
    }
}
