using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Models.Home;
using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Statistics;

namespace SneakersShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ISneakerService sneakers;

        public HomeController(IStatisticsService statistics, ISneakerService sneakers)
        {
            this.statistics = statistics;
            this.sneakers = sneakers;
        }

        public IActionResult Index()
        {
            var latestSneakers = sneakers.Latest();

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