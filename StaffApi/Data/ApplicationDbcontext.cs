using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StaffApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace StaffApi.Data
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
            : base(options)
        {
        }
    }
}
