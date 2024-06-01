using Afri_Energy_Connect.Models;
using Microsoft.EntityFrameworkCore;

namespace Afri_Energy_Connect.Services
{
    public class FarmerDbContext : DbContext
    {
        public FarmerDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Farmer> Farmers { get; set; }

       
    }



}
