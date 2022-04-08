using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Infrastructure.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInfo(this ISneakerModel sneaker)
            => sneaker.Brand + "-" + sneaker.Model;
    }
}
