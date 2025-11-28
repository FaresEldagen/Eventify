using Eventify.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class EventPhotoConfiguration : IEntityTypeConfiguration<EventPhoto>
    {
        public void Configure(EntityTypeBuilder<EventPhoto> builder)
        {
            builder.ToTable("EventPhotos");

            builder.HasKey(ep => new { ep.PhotoUrl, ep.EventId});

            builder.Property(ep => ep.PhotoUrl)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250).IsRequired();


            // Foreign-Keys
            builder.Property(ep => ep.EventId)
                .HasColumnType("INT").IsRequired();
        }
    }
}
