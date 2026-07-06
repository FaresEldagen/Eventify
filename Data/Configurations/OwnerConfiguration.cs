using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {

            builder.Property(o => o.VenueCount)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(o => o.WithdrawableEarnings)
                .HasColumnType("DECIMAL")
                .HasPrecision(18, 2)
                .IsRequired();

            //NavigationProperty
            builder.HasMany(o => o.Venues).WithOne(s => s.Owner).HasForeignKey(s => s.OwnerId);


        }
    }
}
