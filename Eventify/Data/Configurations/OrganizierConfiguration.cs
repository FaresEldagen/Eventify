using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class OrganizierConfiguration : IEntityTypeConfiguration<Organizier> 
    {
        public void Configure(EntityTypeBuilder<Organizier> builder)
        {
            builder.ToTable("Organiziers");

            builder.HasKey(e => e.OrganizierId);

            builder.Property(e => e.Password)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255).IsRequired();

            builder.Property(e => e.Photo)
                .HasColumnType("VARCHAR").HasMaxLength(150);

            builder.Property(e => e.Username)
                .HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();

            builder.Property(e => e.Gender)
                .HasConversion<int>().IsRequired();

            builder.Property(e => e.BIO)
                .HasColumnType("VARCHAR(MAX)");

            builder.Property(e => e.Country)
                .HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();

            builder.Property(e => e.ExperienceYear)
                .HasColumnType("INT").HasMaxLength(50);

            builder.Property(e => e.PastEventCount)
                .HasColumnType("INT").HasMaxLength(50);

            builder.Property(e => e.Specialization)
                .HasColumnType("VARCHAR").HasMaxLength(50);

            builder.Property(e => e.Email)
                .HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();

            builder.Property(e => e.FrontIDPhoto)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.Property(e => e.BackIDPhoto)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.Property(e => e.NationalIDNumber)
                .HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            builder.Property(e => e.ArabicAddress)
                .HasColumnType("VARCHAR").HasMaxLength(255);

            builder.Property(e => e.ArabicFullName)
                .HasColumnType("VARCHAR").HasMaxLength(100);

            builder.Property(e => e.CellPhone)
                .HasColumnType("VARCHAR").HasMaxLength(11).IsRequired();

            builder.Property(e => e.JoinedDate)
                .HasColumnType("DATETIME2").IsRequired();
        }

    }

}
