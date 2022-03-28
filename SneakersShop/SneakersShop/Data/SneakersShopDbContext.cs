using Microsoft.AspNetCore.Identity;
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

        public DbSet<Seller> Sellers { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sneakers>()
                .HasOne(c => c.Category)
                .WithMany(t => t.Sneakers)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sneakers>()
                .HasOne(s => s.Seller)
                .WithMany(s => s.Sneakers)
                .HasForeignKey(s => s.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Seller>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}