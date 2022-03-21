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

        public IActionResult All([FromQuery]AllSneakersQueryModel query)
        {
            var sneakersQuery = this.data.Sneakers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                sneakersQuery = sneakersQuery.Where(s => s.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                sneakersQuery = sneakersQuery.Where(s =>
                (s.Brand + " " + s.Model).ToLower().Contains(query.SearchTerm.ToLower())
                || s.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            sneakersQuery = query.Sorting switch
            {
                SneakersSorting.Price => sneakersQuery.OrderByDescending(s => s.Price),
                SneakersSorting.BrandAndModel => sneakersQuery.OrderBy(s => s.Brand).ThenBy(s => s.Model),
                SneakersSorting.ReleaseDate or _ => sneakersQuery.OrderByDescending(s => s.Id)
            };

            var totalSneakers = sneakersQuery.Count();

            var sneakers = sneakersQuery
                .Skip((query.CurrentPage - 1) * AllSneakersQueryModel.SneakersPerPage)
                .Take(AllSneakersQueryModel.SneakersPerPage)
                .Select(s => new SneakersListingViewModel
                {
                    Id = s.Id,
                    Brand = s.Brand,
                    Model = s.Model,
                    Color = s.Color,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl,
                    Category = s.Category.Name
                })
                .ToList();

            var sneakersBrands = this.data
                .Sneakers
                .Select(s => s.Brand)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            query.TotalSneakers = totalSneakers;
            query.Brands = sneakersBrands;
            query.Sneakers = sneakers;

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
