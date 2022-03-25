using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Data.Models;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers;

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

        public IActionResult All([FromQuery]AllSneakersQueryModel query)
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

            return RedirectToAction(nameof(All));
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
