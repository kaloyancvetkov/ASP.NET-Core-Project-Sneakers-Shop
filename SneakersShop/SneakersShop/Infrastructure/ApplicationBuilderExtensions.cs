using Microsoft.EntityFrameworkCore;
using SneakersShop.Data;
using SneakersShop.Data.Models;

namespace SneakersShop.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<SneakersShopDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(SneakersShopDbContext data)
        {
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

    }
}
