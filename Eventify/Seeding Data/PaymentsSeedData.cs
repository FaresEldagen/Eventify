using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Seeding_Data
{
    public class PaymentsSeedData : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(
                new Payment
                {
                    Id = 4,
                    PaymentDate = new DateTime(2025, 7, 8),
                    Amount = 200m,
                    Reference = "PAY-EVT4-001",
                    EventName = "Alexandria Summer Music Festival",
                    status = PaymentStatusEnum.Completed,
                    EventId = 4
                },
                new Payment
                {
                    Id = 6,
                    PaymentDate = new DateTime(2025, 8, 30),
                    Amount = 800m,
                    Reference = "PAY-EVT6-001",
                    EventName = "Abu Dhabi Luxury Expo",
                    status = PaymentStatusEnum.Completed,
                    EventId = 6
                },
                new Payment
                {
                    Id = 7,
                    PaymentDate = new DateTime(2025, 2, 18),
                    Amount = 100m,
                    Reference = "PAY-EVT7-001",
                    EventName = "Jeddah Beach Charity Run",
                    status = PaymentStatusEnum.Completed,
                    EventId = 7
                },
                new Payment
                {
                    Id = 8,
                    PaymentDate = new DateTime(2025, 8, 13),
                    Amount = 350m,
                    Reference = "PAY-EVT8-001",
                    EventName = "Doha Tech Workshop",
                    status = PaymentStatusEnum.Completed,
                    EventId = 8
                },
                new Payment
                {
                    Id = 10,
                    PaymentDate = new DateTime(2025, 10, 8),
                    Amount = 220m,
                    Reference = "PAY-EVT10-001",
                    EventName = "Sharjah Digital Art Expo",
                    status = PaymentStatusEnum.Completed,
                    EventId = 10
                }
            );
        }
    }
}
