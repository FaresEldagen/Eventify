using Eventify.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Data.Configurations
{
    public class PaymentConfiguaration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentDate)
                .HasColumnType("DATETIME2").IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnType("DECIMAL")
                .HasPrecision(18, 2).IsRequired();

            builder.Property(p => p.Reference)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100).IsRequired(false);

            builder.Property(p => p.status)
                .HasConversion<int>().IsRequired();


            // Foreign-Keys
            builder.Property(p => p.EventId)
                .HasColumnType("INT");

            // Relationships 
            // Payment with Event
            builder.HasOne(p => p.Event)
                .WithOne(e => e.Payment)
                .HasForeignKey<Payment>(p => p.EventId)
                .IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
