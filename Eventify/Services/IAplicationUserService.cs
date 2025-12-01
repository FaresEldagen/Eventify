namespace Eventify.Services
{
    public interface IApplicationUserService
    {
        public int AddMonyToOwnerById(int ownerId, decimal amount);
    }
}
