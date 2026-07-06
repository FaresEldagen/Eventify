using Eventify.Models.Dtos;
using Eventify.Models.Entities;

namespace Eventify.Services
{
    public interface IApplicationUserService
    {
        public int AddMonyToOwnerById(int ownerId, decimal amount);

        public int WithdrawMonyFromOwnerById(int ownerId);

        public Task<int> MakeUserAdminByEmailAsync(string userEmail);
        public Task<List<ApplicationUser>> GetAllUserByRoleAsync(string role);
        public List<AdminViewUsersDto>GetApplicationUsers();
    }
}
