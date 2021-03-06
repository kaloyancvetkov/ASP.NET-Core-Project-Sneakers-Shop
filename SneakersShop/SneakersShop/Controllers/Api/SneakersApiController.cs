using Microsoft.AspNetCore.Mvc;
using SneakersShop.Controllers.Api.Sneakers;
using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Controllers.Api
{
    [ApiController]
    [Route("api/sneakers")]
    public class SneakersApiController : ControllerBase
    {
        private readonly ISneakerService sneakers;

        public SneakersApiController(ISneakerService sneakers)
            => this.sneakers = sneakers;

        [HttpGet]
        public SneakerQueryServiceModel All([FromQuery] AllSneakersApiRequestModel query)
            => this.sneakers.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.SneakersPerPage);
    }
}
