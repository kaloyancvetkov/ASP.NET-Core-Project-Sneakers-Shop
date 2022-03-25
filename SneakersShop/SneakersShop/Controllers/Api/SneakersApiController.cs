using Microsoft.AspNetCore.Mvc;
using SneakersShop.Controllers.Api.Sneakers;
using SneakersShop.Data;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers;

namespace SneakersShop.Controllers.Api
{
    [ApiController]
    [Route("api/sneakers")]
    public class SneakersApiController : ControllerBase
    {
        private readonly ISneakersService sneakers;

        public SneakersApiController(ISneakersService sneakers)
            => this.sneakers = sneakers;

        [HttpGet]
        public SneakersQueryServiceModel All([FromQuery] AllSneakersApiRequestModel query)
            => this.sneakers.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.SneakersPerPage);
    }
}
