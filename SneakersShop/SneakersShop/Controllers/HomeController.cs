using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SneakersShop.Models.Home;
using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Sneakers.Models;
using SneakersShop.Services.Statistics;

namespace SneakersShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ISneakerService sneakers;
        private readonly IMemoryCache cache;

        public HomeController(IStatisticsService statistics, ISneakerService sneakers, IMemoryCache cache)
        {
            this.statistics = statistics;
            this.sneakers = sneakers;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string LatestSneakersCacheKey = "LatestSneakersCacheKey";

            var latestSneakers = this.cache.Get<List<LatestSneakerServiceModel>>(LatestSneakersCacheKey);

            if (latestSneakers == null)
            {
                latestSneakers = this.sneakers
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestSneakersCacheKey, latestSneakers, cacheOptions);
            }

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalSneakers = totalStatistics.TotalSneakers,
                TotalUsers = totalStatistics.TotalUsers,
                Sneakers = latestSneakers.ToList()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}