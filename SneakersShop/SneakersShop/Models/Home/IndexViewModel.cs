using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Models.Home
{
    public class IndexViewModel
    {
        public int TotalSneakers { get; init; }

        public int TotalUsers { get; init; }

        public int TotalPurchases { get; init; }

        public List<LatestSneakerServiceModel> Sneakers { get; init; }
    }
}
