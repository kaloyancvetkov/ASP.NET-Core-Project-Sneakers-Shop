using Microsoft.AspNetCore.Mvc;
using SneakersShop.Data;
using SneakersShop.Data.Models;

namespace SneakersShop.Controllers.Api
{
    [ApiController]
    [Route("api/sneakers")]
    public class SneakersApiController : ControllerBase
    {
        private readonly SneakersShopDbContext data;

        public SneakersApiController(SneakersShopDbContext data)
            => this.data = data;


    }
}
