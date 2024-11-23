using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffApi.Data;
using StaffApi.Entities;

namespace StaffApi.Buisness.UserCheck
{
    public class UserRoleCheck : IUserRoleCheck
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;

        public UserRoleCheck(UserManager<ApplicationUser> userManager,AppDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<bool> checkUser(string userID)
        {
            var user = await userManager.FindByIdAsync(userID);
            if (user == null) return false;
            else
                return true;

        }
        public async Task<bool> checkrole(string userID)
        {
            var user = await userManager.FindByIdAsync(userID);
            var role = await userManager.IsInRoleAsync(user, "Registerar");
            if (!role) return false;
            // Forbid("You have no permissions to edit student information");
            return true;

        }

        public bool checkRoleName(string RoleName)
        {
            var roles = context.Roles.ToList();
            var result=true;
            foreach (var role in roles)
            {
                if (role.Name.ToUpper() == RoleName.ToUpper()) {
                    result = true;
                    break;
                }
                else result = false;
            }
            return result;
        }
    }
}
