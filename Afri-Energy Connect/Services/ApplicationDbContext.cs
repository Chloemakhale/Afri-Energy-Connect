using Afri_Energy_Connect.Models;
using Microsoft.EntityFrameworkCore;

namespace Afri_Energy_Connect.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
