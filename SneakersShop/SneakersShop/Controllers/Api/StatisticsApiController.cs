using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Models.Api.Statistics;

namespace SneakersShop.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly SneakersShopDbContext data;

        public StatisticsApiController(SneakersShopDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticResponseModel GetStatistics()
        {
            var totalSneakers = this.data.Sneakers.Count();
            var totalUsers = this.data.Users.Count();
            return new StatisticResponseModel
            {
                TotalSneakers = totalSneakers,
                TotalUsers = totalUsers,
                TotalPurchases = 0
            };
        }
    }
}
