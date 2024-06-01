using Afri_Energy_Connect.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Afri_Energy_Connect.Data;

public class Afri_Energy_ConnectContext : IdentityDbContext<Afri_Energy_ConnectUser>
{
    public Afri_Energy_ConnectContext(DbContextOptions<Afri_Energy_ConnectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
