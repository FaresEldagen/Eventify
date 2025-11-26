using Eventify.Models.Enums;

namespace Eventify.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? Reference { get; set; }
        public string? EventName { get; set; }
        public PaymentStatusEnum status { get; set; }



        // Navigation Property
        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}
