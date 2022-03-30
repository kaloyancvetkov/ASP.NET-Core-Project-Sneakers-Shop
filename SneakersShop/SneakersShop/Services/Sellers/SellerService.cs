using SneakersShop.Data;

namespace SneakersShop.Services.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly SneakersShopDbContext data;

        public SellerService(SneakersShopDbContext data)
            => this.data = data;

        public bool IsSeller(string userId)
            => this.data
            .Sellers
            .Any(s => s.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Sellers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefault();
    }
}
