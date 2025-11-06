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


            // Foreign-Keys
            builder.Property(e => e.VenueId)
                .HasColumnType("INT").IsRequired();

            builder.Property(e => e.OrganizerId)
                .HasColumnType("INT").IsRequired();

        }
    }
}
