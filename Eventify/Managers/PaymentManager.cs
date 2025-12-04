using Eventify.Data;
using Eventify.Models.Entities;
using Eventify.Services;

namespace Eventify.Managers
{
    public class PaymentManager : IPaymentService
    {
        private readonly AppDbContext _db;

        public PaymentManager(AppDbContext db)
        {
            _db = db;
        }



        public List<Payment> Get3()
        {
            throw new NotImplementedException();
        }

        public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Payment? GetByIdWithIncludes(int id)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetByUserId(int id)
        {
            throw new NotImplementedException();
        }



        public int Insert(Payment payment)
        {
            _db.Payments.Add(payment);
            return _db.SaveChanges();
        }



        public int Update(Payment obj)
        {
            var oldPayment = _db.Payments.FirstOrDefault(p => p.Id == obj.Id);
            if (oldPayment == null)
                return -1;
            oldPayment.EventName = obj.EventName;
            oldPayment.PaymentDate = obj.PaymentDate;
            oldPayment.status = obj.status;
            oldPayment.Reference = obj.Reference;

            return _db.SaveChanges();
        }



        public int Delete(int id)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.Id == id);
            var deletedPayment = _db.Payments.Remove(payment);
            if (deletedPayment == null)
                return -1;
            _db.SaveChanges();
            return 1;
        }
    }
}
