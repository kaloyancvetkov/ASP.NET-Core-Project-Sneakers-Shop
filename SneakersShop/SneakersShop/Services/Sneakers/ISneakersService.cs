using SneakersShop.Models.Sneakers;

namespace SneakersShop.Services.Sneakers
{
    public interface ISneakersService
    {
        SneakersQueryServiceModel All(string brand, string searchTerm, SneakersSorting sorting, int currentPage, int sneakersPerPage);

        IEnumerable<string> AllSneakersBrands();
    }
}
