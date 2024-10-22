using ASAP_Task.WebAPI.Authentication.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASAP_Task.WepApi.Authentication.Context
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
    }
}
