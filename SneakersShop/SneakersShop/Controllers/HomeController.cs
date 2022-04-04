using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Models;
using SneakersShop.Models.Home;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Statistics;
using System.Diagnostics;

namespace SneakersShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly SneakersShopDbContext data;
        private readonly IMapper mapper;

        public HomeController(
            IStatisticsService statistics,
            SneakersShopDbContext data, IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var sneakers = this.data
                .Sneakers
                .OrderByDescending(s => s.Id)
                .ProjectTo<SneakersIndexViewModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalSneakers = totalStatistics.TotalSneakers,
                TotalUsers = totalStatistics.TotalUsers,
                Sneakers = sneakers
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}