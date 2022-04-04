using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static SneakersShop.Areas.Admin.AdminConstants;

namespace SneakersShop.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class SneakersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
