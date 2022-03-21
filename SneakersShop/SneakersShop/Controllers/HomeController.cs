using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Models;
using SneakersShop.Models.Home;
using SneakersShop.Models.Sneakers;
using System.Diagnostics;

namespace SneakersShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly SneakersShopDbContext data;

        public HomeController(SneakersShopDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalSneakers = this.data.Sneakers.Count();

            var sneakers = this.data
                .Sneakers
                .OrderByDescending(s => s.Id)
                .Select(s => new SneakersIndexViewModel
                {
                    Id = s.Id,
                    Brand = s.Brand,
                    Model = s.Model,
                    Color = s.Color,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl,
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalSneakers = totalSneakers,
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