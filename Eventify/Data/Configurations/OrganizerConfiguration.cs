using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer> 
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {

            builder.Property(e => e.ExperienceYear)
                .HasColumnType("INT").HasMaxLength(50);

            builder.Property(e => e.PastEventCount)
                .HasColumnType("INT").HasMaxLength(50);

            builder.Property(e => e.Specialization)
                .HasColumnType("VARCHAR").HasMaxLength(50);




        }

    }

}
