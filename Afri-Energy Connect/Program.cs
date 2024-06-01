using Afri_Energy_Connect.Services;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using Microsoft.AspNetCore.Identity;
using Afri_Energy_Connect.Areas.Identity.Data;
using Afri_Energy_Connect.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Afri_Energy_Connect
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Read the connection string from configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register ApplicationDbContext with EnableRetryOnFailure
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }));

            // Register AuthDbContext with EnableRetryOnFailure
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }));

            builder.Services.AddDbContext<FarmerDbContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }));

            // Add Identity services
            builder.Services.AddDefaultIdentity<Afri_Energy_ConnectUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AuthDbContext>();

            // Add services to the container
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Add policy to restrict access to only the specified user with correct occupation
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeChloe", policy =>
                    policy.Requirements.Add(new EmployeeRequirement()));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, EmployeeRequirementHandler>();

            var app = builder.Build();

            // Seed the database with the specific user and their occupation
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Afri_Energy_ConnectUser>>();
                await SeedData(userManager);
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            await app.RunAsync();
        }

        private static async Task SeedData(UserManager<Afri_Energy_ConnectUser> userManager)
        {
            string email = "chloe@employee.com";
            string password = "Test012!";
            string occupation = "Employee";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new Afri_Energy_ConnectUser { UserName = email, Email = email };
                await userManager.CreateAsync(user, password);
            }
        }
    }

    public class EmployeeRequirement : IAuthorizationRequirement { }

    public class EmployeeRequirementHandler : AuthorizationHandler<EmployeeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var user = context.User;
            var occupationClaim = user.Claims.FirstOrDefault(c => c.Type == "Occupation")?.Value;
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (emailClaim == "chloe@employee.com" && occupationClaim == "Employee")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
