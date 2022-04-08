using SneakersShop.Models.Home;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Services.Sneakers
{
    public interface ISneakerService
    {
        SneakerQueryServiceModel All(string brand = null, string searchTerm = null, SneakersSorting sorting = SneakersSorting.ReleaseDate, int currentPage = 1,
            int sneakersPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestSneakerServiceModel> Latest();

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
                int categoryId,
                bool isPublic);

        IEnumerable<SneakerServiceModel> ByUser(string userId);

        bool IsBySeller(int sneakerId, int sellerId);

        void ChangeVisibility(int id);

        IEnumerable<string> AllBrands();

        IEnumerable<SneakerCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
