using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Controllers;
using SneakersShop.Data;
using SneakersShop.Data.Models;
using SneakersShop.Infrastructure;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers;
using System.Security.Claims;

namespace TShirtsShop.Controllers
{
    public class SneakersController : Controller
    {
        private readonly SneakersShopDbContext data;
        private readonly ISneakersService sneakers;

        public SneakersController(SneakersShopDbContext data, ISneakersService sneakers)
        {
            this.sneakers = sneakers;
            this.data = data;
        }

        public IActionResult All([FromQuery] AllSneakersQueryModel query)
        {
            var queryResult = this.sneakers.All(query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllSneakersQueryModel.SneakersPerPage);

            var sneakersBrands = this.sneakers.AllSneakersBrands();

            query.TotalSneakers = queryResult.TotalSneakers;
            query.Brands = sneakersBrands;
            query.Sneakers = queryResult.Sneakers;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsSeller())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new AddSneakersFormModel
            {
                Categories = this.GetSneakersCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddSneakersFormModel sneakers)
        {
            var sellerId = this.data
                .Sellers
                .Where(s => s.UserId == this.User.GetId())
                .Select(s => s.Id)
                .FirstOrDefault();

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.data.Categories.Any(c => c.Id == sneakers.CategoryId))
            {
                this.ModelState.AddModelError(nameof(sneakers.CategoryId), "Category does not exist.");
            }

            sneakers.Categories = this.GetSneakersCategories();

            if (!ModelState.IsValid)
            {
                sneakers.Categories = this.GetSneakersCategories();

                return View(sneakers);
            }

            var sneakersData = new Sneakers
            {
                Brand = sneakers.Brand,
                Model = sneakers.Model,
                Color = sneakers.Color,
                Description = sneakers.Description,
                ImageUrl = sneakers.ImageUrl,
                Price = sneakers.Price,
                CategoryId = sneakers.CategoryId,
                SellerId = sellerId
            };

            this.data.Sneakers.Add(sneakersData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsSeller()
            => this.data
            .Sellers
            .Any(s => s.UserId == this.User.GetId());

        private IEnumerable<SneakersCategoryViewModel> GetSneakersCategories()
            => this.data
            .Categories
            .Select(c => new SneakersCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();
    }
}
