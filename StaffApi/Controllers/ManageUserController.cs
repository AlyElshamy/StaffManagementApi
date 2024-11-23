using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffApi.Buisness.UserCheck;
using StaffApi.Data;
using StaffApi.DTOs;
using StaffApi.Entities;
using StaffApi.Interfaces;
using StaffApi.ViewModels;
using System.Net.Mail;

namespace StaffApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;
        private readonly IUserRoleCheck userRoleCheck;

        public ManageUserController( UserManager<ApplicationUser> userManager,AppDbContext context,IUserRoleCheck userRoleCheck)
        {
            this.userManager = userManager;
            this.context = context;
            this.userRoleCheck = userRoleCheck;
        }
        [HttpPost]
        [Route("ChangeRole")]
        public async Task<object> PostChangeRole(ManageUserVM usermodel)
        {
            var user = await userManager.FindByIdAsync(usermodel.UserId);
            if (user == null) return Forbid("user not Found");
            if(! userRoleCheck.checkRoleName(usermodel.RoleName)) return BadRequest("please Enter Valid Role Name");
            var currentRole = await userManager.GetRolesAsync(user);
           
            if (currentRole != null && currentRole.Contains(usermodel.RoleName))
                return "Current User Hase The same Role";
            else
            {
                await userManager.RemoveFromRoleAsync(user,currentRole.FirstOrDefault());
                await userManager.AddToRoleAsync(user, usermodel.RoleName);

            }
            //var role = await userManager.IsInRoleAsync(user, "Registrar");
            context.SaveChanges();
            return $"Current Role is {usermodel.RoleName}";
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterVM RegisterModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = new MailAddress(RegisterModel.Email).User,
                    Email = RegisterModel.Email,
                    firstName = RegisterModel.FirstName,
                    lastName = RegisterModel.LastName
                };
                var result = await userManager.CreateAsync(user, RegisterModel.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return Ok(new { user.firstName, user.Email, user.lastName });
                }
                else
                    return BadRequest("Something went Error");

            }
            else
                return BadRequest("Something went Error");

        }
    }
}
