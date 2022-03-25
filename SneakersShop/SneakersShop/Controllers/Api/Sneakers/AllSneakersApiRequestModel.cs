using SneakersShop.Models.Sneakers;

namespace SneakersShop.Controllers.Api.Sneakers
{
    public class AllSneakersApiRequestModel
    {
        public string ?Brand { get; init; }

        public string ?SearchTerm { get; init; }

        public SneakersSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int SneakersPerPage { get; init; } = 10;

        public int TotalSneakers { get; init; }
    }
}
