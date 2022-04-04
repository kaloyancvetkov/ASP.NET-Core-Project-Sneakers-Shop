using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Services.Sneakers
{
    public interface ISneakerService
    {
        SneakerQueryServiceModel All(string brand, string searchTerm, SneakersSorting sorting, int currentPage, int sneakersPerPage);

        SneakerDetailsServiceModel Details(int sneakerId);

        int Create(string brand,
                string model,
                string color,
                string description,
                string imageUrl,
                decimal price,
                int categoryId,
                int sellerId);

        bool Edit(
            int sneakerId,
            string brand,
                string model,
                string color,
                string description,
                string imageUrl,
                decimal price,
                int categoryId);

        IEnumerable<SneakerServiceModel> ByUser(string userId);

        bool IsBySeller(int sneakerId, int sellerId);

        IEnumerable<string> AllBrands();

        IEnumerable<SneakerCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
