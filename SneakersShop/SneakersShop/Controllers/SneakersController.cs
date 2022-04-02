using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Controllers;
using SneakersShop.Infrastructure;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sellers;
using SneakersShop.Services.Sneakers;

namespace TShirtsShop.Controllers
{
    public class SneakersController : Controller
    {
        private readonly ISellerService sellers;
        private readonly ISneakerService sneakers;

        public SneakersController(ISellerService sellers, ISneakerService sneakers)
        {
            this.sneakers = sneakers;
            this.sellers = sellers;
        }

        public IActionResult All([FromQuery] AllSneakersQueryModel query)
        {
            var queryResult = this.sneakers.All(query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllSneakersQueryModel.SneakersPerPage);

            var sneakersBrands = this.sneakers.AllBrands();

            query.TotalSneakers = queryResult.TotalSneakers;
            query.Brands = sneakersBrands;
            query.Sneakers = queryResult.Sneakers;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var mySneakers = this.sneakers.ByUser(this.User.Id());

            return View(mySneakers);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.sellers.IsSeller(this.User.Id()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new SneakerFormModel
            {
                Categories = this.sneakers.AllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(SneakerFormModel sneaker)
        {
            var sellerId = this.sellers.IdByUser(this.User.Id());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.sneakers.CategoryExists(sneaker.CategoryId))
            {
                this.ModelState.AddModelError(nameof(sneaker.CategoryId), "Category does not exist.");
            }

            sneaker.Categories = this.sneakers.AllCategories();

            if (!ModelState.IsValid)
            {
                sneaker.Categories = this.sneakers.AllCategories();

                return View(sneakers);
            }

            this.sneakers.Create(
                sneaker.Brand,
                sneaker.Model,
                sneaker.Color,
                sneaker.Description,
                sneaker.ImageUrl,
                sneaker.Price,
                sneaker.CategoryId,
                sellerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.sellers.IsSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var sneaker = this.sneakers.Details(id);

            if (sneaker.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new SneakerFormModel
            {
                Brand = sneaker.Brand,
                Model = sneaker.Model,
                Description = sneaker.Description,
                ImageUrl = sneaker.ImageUrl,
                Color = sneaker.Color,
                Price = sneaker.Price,
                CategoryId = sneaker.CategoryId,
                Categories = this.sneakers.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, SneakerFormModel sneaker)
        {
            var sellerId = this.sellers.IdByUser(this.User.Id());

            if (sellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.sneakers.CategoryExists(sneaker.CategoryId))
            {
                this.ModelState.AddModelError(nameof(sneaker.CategoryId), "Category does not exist.");
            }

            sneaker.Categories = this.sneakers.AllCategories();

            if (!ModelState.IsValid)
            {
                sneaker.Categories = this.sneakers.AllCategories();

                return View(sneakers);
            }

            if (!this.sneakers.IsBySeller(id, sellerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.sneakers.Edit(
                id,
                sneaker.Brand,
                sneaker.Model,
                sneaker.Color,
                sneaker.Description,
                sneaker.ImageUrl,
                sneaker.Price,
                sneaker.CategoryId);


            return RedirectToAction(nameof(All));
        }
    }
}
