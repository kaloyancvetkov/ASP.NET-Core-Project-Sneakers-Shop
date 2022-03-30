using SneakersShop.Data;
using SneakersShop.Models.Sneakers;
using SneakersShop.Data.Models;

namespace SneakersShop.Services.Sneakers
{
    public class SneakerService : ISneakerService
    {
        private readonly SneakersShopDbContext data;

        public SneakerService(SneakersShopDbContext data)
            => this.data = data;

        public SneakerQueryServiceModel All(string brand, string searchTerm, SneakersSorting sorting, int currentPage, int sneakersPerPage)
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

            var sneakers = GetSneakers(sneakersQuery
                .Skip((currentPage - 1) * sneakersPerPage)
                .Take(sneakersPerPage));

            return new SneakerQueryServiceModel
            {
                TotalSneakers = totalSneakers,
                CurrentPage = currentPage,
                SneakersPerPage = sneakersPerPage,
                Sneakers = sneakers
            };
        }

        public IEnumerable<string> AllBrands()
            => this.data
                .Sneakers
                .Select(s => s.Brand)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

        public IEnumerable<SneakerCategoryServiceModel> AllCategories()
            => this.data
            .Categories
            .Select(c => new SneakerCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();

        public IEnumerable<SneakerServiceModel> ByUser(string userId)
            => this.GetSneakers(this.data
                .Sneakers
                .Where(s => s.Seller.UserId == userId));


        public bool IsBySeller(int sneakerId, int sellerId)
            => this.data
            .Sneakers
            .Any(s => s.Id == sneakerId && s.SellerId == sellerId);

        public bool CategoryExists(int categoryId)
            => this.data.Categories.Any(c => c.Id == categoryId);

        public int Create(string brand, string model, string color, string description, string imageUrl, decimal price, int categoryId, int sellerId)
        {


            var sneakersData = new Sneaker
            {
                Brand = brand,
                Model = model,
                Color = color,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                CategoryId = categoryId,
                SellerId = sellerId
            };

            this.data.Sneakers.Add(sneakersData);
            this.data.SaveChanges();

            return sneakersData.Id;
        }

        public bool Edit(int id, string brand, string model, string color, string description, string imageUrl, decimal price, int categoryId)
        {
            var sneakerData = this.data
                .Sneakers
                .Find(id);

            if (sneakerData == null)
            {
                return false;
            }

            sneakerData.Brand = brand;
                sneakerData.Model = model;
                sneakerData.Color = color;
            sneakerData.Description = description;
            sneakerData.ImageUrl = imageUrl;
            sneakerData.Price = price;
            sneakerData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public SneakerDetailsServiceModel Details(int id)
            => this.data
            .Sneakers
            .Where(s => s.Id == id)
            .Select(s => new SneakerDetailsServiceModel
            {
                Id = s.Id,
                Brand = s.Brand,
                Model = s.Model,
                Description = s.Description,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                CategoryName = s.Category.Name,
                SellerId = s.SellerId,
                SellerName = s.Seller.Name,
                UserId = s.Seller.UserId
            })
            .FirstOrDefault();

        private IEnumerable<SneakerServiceModel> GetSneakers(IQueryable<Sneaker> sneakersQuery)
            => sneakersQuery
            .Select(s => new SneakerServiceModel
            {
                Id = s.Id,
                Brand = s.Brand,
                Model = s.Model,
                Color = s.Color,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
                CategoryName = s.Category.Name
            })
            .ToList();
    }
}
