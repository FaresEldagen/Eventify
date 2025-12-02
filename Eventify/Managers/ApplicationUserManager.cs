using Eventify.Data;
using Eventify.Services;

namespace Eventify.Managers
{
    public class ApplicationUserManager : IApplicationUserService
    {
        private readonly AppDbContext _db;

        public ApplicationUserManager(AppDbContext db)
        {
            _db = db;
        }

        public int AddMonyToOwnerById(int ownerId, decimal amount)
        {
            
            var owner = _db.Owners.FirstOrDefault(o => o.Id == ownerId);
            owner!.WithdrawableEarnings += amount;
            return _db.SaveChanges();
        }

        public int WithdrawMonyFromOwnerById(int ownerId)
        {
            var owner = _db.Owners.FirstOrDefault(o => o.Id == ownerId);
            if (owner == null)
                return 0;
            owner.WithdrawableEarnings = 0;
            return _db.SaveChanges();
        }
    }
}
