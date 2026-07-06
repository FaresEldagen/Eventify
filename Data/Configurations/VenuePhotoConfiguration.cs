using Eventify.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class VenuePhotoConfiguration : IEntityTypeConfiguration<VenuePhoto>
    {
        public void Configure(EntityTypeBuilder<VenuePhoto> builder)
        {
            builder.ToTable("VenuePhotos");

            builder.HasKey(vp => new { vp.PhotoUrl, vp.VenueId });

            builder.Property(vp => vp.PhotoUrl)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250).IsRequired();


            // Foreign-Keys
            builder.Property(vp => vp.VenueId)
                    .HasColumnType("INT").IsRequired();
        }
    }
}

