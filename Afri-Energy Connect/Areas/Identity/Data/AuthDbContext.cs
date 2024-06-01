using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Afri_Energy_Connect.Areas.Identity.Data;

namespace AuthSystem.Data
{
    public class AuthDbContext : IdentityDbContext<Afri_Energy_ConnectUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }
    }
}
