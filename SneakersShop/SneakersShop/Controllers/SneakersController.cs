using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Data.Models;
using SneakersShop.Models.Sneakers;

namespace TShirtsShop.Controllers
{
    public class SneakersController : Controller
    {
        private readonly SneakersShopDbContext data;

        public SneakersController(SneakersShopDbContext data) => this.data = data;

        public IActionResult Add() => View(new AddSneakersFormModel
        {
            Categories = this.GetSneakersCategories()
        });

        [HttpPost]
        public IActionResult Add(AddSneakersFormModel sneakers)
        {
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
            };

            this.data.Sneakers.Add(sneakersData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

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
