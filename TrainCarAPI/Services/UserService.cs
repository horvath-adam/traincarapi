using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;

namespace TrainCarAPI.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(RoleManager<IdentityRole<int>> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitRoles()
        {
            await CreateRole(Roles.ADMIN);
            await CreateRole(Roles.USER);
        }

        public async Task InitUsers()
        {
            await CreateUser("mav_admin@test.com", "mav_admin", new DateTime(2000, 1, 1), true, "MAV", "Admin_1234", Roles.ADMIN);
            await CreateUser("mav_user@test.com", "mav_user", new DateTime(1998, 05, 17), true, "MAV", "User_1234", Roles.USER);
        }

        private async Task CreateUser(string email, string userName, DateTime dateOfBirth, 
                                      bool isRailwayWorker, string railwayCompanyName,
                                      string password, string role)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                Email = email,
                UserName = userName,
                EmailConfirmed = true,
                DateOfBirth = dateOfBirth,
                IsRailwayWorker = isRailwayWorker,
                RailwayCompanyName = railwayCompanyName

            };
            var user = await _userManager.CreateAsync(admin, password);
            if (user.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, role);
                await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, role));
            }
        }
        private async Task CreateRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }
}
