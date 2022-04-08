using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Data.Models;
using SneakersShop.Infrastructure;
using SneakersShop.Models.Sellers;
using static SneakersShop.WebConstants;

namespace SneakersShop.Controllers
{
    public class SellersController : Controller
    {
        private readonly SneakersShopDbContext data;

        public SellersController(SneakersShopDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.Id();

            var userIsAlreadySeller = this.data
                .Sellers
                .Any(s => s.UserId == userId);

            if (userIsAlreadySeller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new Seller
            {
                Name = seller.Name,
                PhoneNumber = seller.PhoneNumber,
                UserId = userId
            };

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becoming a seller!";

            return RedirectToAction("All", "Sneakers");
        }
    }
}
