using SneakersShop.Data;

namespace SneakersShop.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly SneakersShopDbContext data;

        public StatisticsService(SneakersShopDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalSneakers = this.data.Sneakers.Count(s => s.isPublic);
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalSneakers = totalSneakers,
                TotalUsers = totalUsers,
            };
        }
    }
}
