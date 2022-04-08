using Microsoft.AspNetCore.Mvc;
using SneakersShop.Services.Sneakers;

namespace SneakersShop.Areas.Admin.Controllers
{
    public class SneakersController : AdminController
    {
        private readonly ISneakerService sneakers;

        public SneakersController(ISneakerService sneakers) => this.sneakers = sneakers;

        public IActionResult All()
        {
            var sneakers = this.sneakers
                .All(publicOnly: false)
                .Sneakers
                .OrderBy(s => s.Id);

            return View(sneakers);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.sneakers.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
