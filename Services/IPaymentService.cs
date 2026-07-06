using Eventify.Models.Entities;

namespace Eventify.Services
{
    public interface IPaymentService : IGenericService<Payment>
    {
        public List<Payment> GetPayments();
    }
}
