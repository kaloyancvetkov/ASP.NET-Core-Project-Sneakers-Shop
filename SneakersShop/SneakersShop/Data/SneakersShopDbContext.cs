using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Data.Models;

namespace SneakersShop.Data
{
    public class SneakersShopDbContext : IdentityDbContext
    {


        public SneakersShopDbContext(DbContextOptions<SneakersShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sneakers> Sneakers { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sneakers>()
                .HasOne(c => c.Category)
                .WithMany(t => t.Sneakers)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}