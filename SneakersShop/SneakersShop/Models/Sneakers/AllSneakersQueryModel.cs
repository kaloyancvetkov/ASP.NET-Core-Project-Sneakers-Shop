using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Sneakers.Models;
using System.ComponentModel.DataAnnotations;

namespace SneakersShop.Models.Sneakers
{
    public class AllSneakersQueryModel
    {
        public const int SneakersPerPage = 3;

        public string Brand { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public SneakersSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalSneakers { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<SneakerServiceModel> Sneakers { get; set; }
    }
}
