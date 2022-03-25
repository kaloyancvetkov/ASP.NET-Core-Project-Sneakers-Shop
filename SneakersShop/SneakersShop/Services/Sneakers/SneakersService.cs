using SneakersShop.Data;
using SneakersShop.Models.Sneakers;

namespace SneakersShop.Services.Sneakers
{
    public class SneakersService : ISneakersService
    {
        private readonly SneakersShopDbContext data;

        public SneakersService(SneakersShopDbContext data)
            => this.data = data;

        public SneakersQueryServiceModel All(string brand, string searchTerm, SneakersSorting sorting, int currentPage, int sneakersPerPage)
        {
            var sneakersQuery = this.data.Sneakers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                sneakersQuery = sneakersQuery.Where(s => s.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sneakersQuery = sneakersQuery.Where(s =>
                (s.Brand + " " + s.Model).ToLower().Contains(searchTerm.ToLower())
                || s.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            sneakersQuery = sorting switch
            {
                SneakersSorting.Price => sneakersQuery.OrderByDescending(s => s.Price),
                SneakersSorting.BrandAndModel => sneakersQuery.OrderBy(s => s.Brand).ThenBy(s => s.Model),
                SneakersSorting.ReleaseDate or _ => sneakersQuery.OrderByDescending(s => s.Id)
            };

            var totalSneakers = sneakersQuery.Count();

            var sneakers = sneakersQuery
                .Skip((currentPage - 1) * sneakersPerPage)
                .Take(sneakersPerPage)
                .Select(s => new SneakersServiceModel
                {
                    Id = s.Id,
                    Brand = s.Brand,
                    Model = s.Model,
                    Color = s.Color,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl,
                    Category = s.Category.Name
                })
                .ToList();

            return new SneakersQueryServiceModel
            {
                TotalSneakers = totalSneakers,
                CurrentPage = currentPage,
                SneakersPerPage = sneakersPerPage,
                Sneakers = sneakers
            };
        }

        public IEnumerable<string> AllSneakersBrands()
            => this.data
                .Sneakers
                .Select(s => s.Brand)
                .Distinct()
                .OrderBy(s => s)
                .ToList();
    }
}
