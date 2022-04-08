using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Data;
using SneakersShop.Data.Models;
using static SneakersShop.Areas.Admin.AdminConstants;

namespace SneakersShop.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<SneakersShopDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<SneakersShopDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Lifestyle"},
                new Category { Name = "Football"},
                new Category { Name = "Basketball"},
                new Category { Name = "Running"},
                new Category { Name = "Tennis"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@crs.com";
                const string adminPassword = "admin123";

                var user = new IdentityUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
                .GetAwaiter()
                .GetResult();
        }

    }
}
