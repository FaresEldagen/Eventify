using Eventify.Data;
using Eventify.Models.Dtos;
using Eventify.Models.Entities;
using Eventify.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Eventify.Managers
{
    public class ApplicationUserManager : IApplicationUserService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManager(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public int AddMonyToOwnerById(int ownerId, decimal amount)
        {
            
            var owner = _db.Owners.FirstOrDefault(o => o.Id == ownerId);
            owner!.WithdrawableEarnings += amount;
            return _db.SaveChanges();
        }

        public async Task<int> MakeUserAdminByEmailAsync(string userEmail)
        {
            var user =await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
                return -1;

            if (await _userManager.IsInRoleAsync(user,"Admin"))
                return 0;

            var admin = new Admin
            {
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                PasswordHash = user.PasswordHash,
                AccountStatus = user.AccountStatus,
                ConcurrencyStamp = user.ConcurrencyStamp,
                EmailConfirmed = user.EmailConfirmed,
                JoinedDate = user.JoinedDate,
                SecurityStamp = user.SecurityStamp,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                AccessFailedCount = user.AccessFailedCount,
                TwoFactorEnabled = user.TwoFactorEnabled


            };

            using(var transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role!);

                    if (removeRoleResult.Succeeded)
                    {
                        var deleteResult = await _userManager.DeleteAsync(user);

                        if (deleteResult.Succeeded)
                        {
                            var createResult = await _userManager.CreateAsync(admin);
                            if (createResult.Succeeded)
                            {
                                var addRoleResult = await _userManager.AddToRoleAsync(admin, "Admin");
                                if (addRoleResult.Succeeded)
                                {
                                    if(deleteResult.Succeeded && removeRoleResult.Succeeded && createResult.Succeeded && addRoleResult.Succeeded)
                                    {
                                        transaction.Commit();
                                        return 1;
                                    }
                                    transaction.Rollback();
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return -2;
                }
            }
            return -2;
        }

        public int WithdrawMonyFromOwnerById(int ownerId)
        {
            var owner = _db.Owners.FirstOrDefault(o => o.Id == ownerId);
            if (owner == null)
                return 0;
            owner.WithdrawableEarnings = 0;
            return _db.SaveChanges();
        }

        public async Task<List<ApplicationUser>> GetAllUserByRoleAsync(string role)
        {
            var usersInRole = (await _userManager.GetUsersInRoleAsync(role)).ToList();
            return usersInRole;
        }

        public List<AdminViewUsersDto> GetApplicationUsers()
        {
           var users = _db.Users.Where(u=>u.AccountStatus == Models.Enums.AccountStatus.Pending)
                        .Join(_db.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u.Id, u.UserName, u.Email, ur.RoleId })
                        .Join(_db.Roles, uur => uur.RoleId, r => r.Id, (uur, r) => new { uur.Id, uur.UserName, uur.Email, Role=r.Name }).Select(j => new AdminViewUsersDto
                        {
                            Id = j.Id,
                            UserName = j.UserName,
                            Email = j.Email,
                            Role = j.Role
                        })
                        .ToList();

            return users;
            
        }
    }
}
