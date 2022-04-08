using SneakersShop.Data;
using SneakersShop.Models.Sneakers;
using SneakersShop.Data.Models;
using SneakersShop.Services.Sneakers.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace SneakersShop.Services.Sneakers
{
    public class SneakerService : ISneakerService
    {
        private readonly SneakersShopDbContext data;
        private readonly IMapper mapper;

        public SneakerService(SneakersShopDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public SneakerQueryServiceModel All(
            string brand = null,
            string searchTerm = null, 
            SneakersSorting sorting = SneakersSorting.ReleaseDate,
            int currentPage = 1,
            int sneakersPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var sneakersQuery = this.data.Sneakers.Where(s => !publicOnly || s.isPublic);

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
            .ProjectTo<SneakerCategoryServiceModel>(this.mapper.ConfigurationProvider)
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
                SellerId = sellerId,
                isPublic = false
            };

            this.data.Sneakers.Add(sneakersData);
            this.data.SaveChanges();

            return sneakersData.Id;
        }

        public bool Edit(int id, 
            string brand, 
            string model, 
            string color, 
            string description, 
            string imageUrl,
            decimal price, 
            int categoryId,
            bool isPublic)
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
            sneakerData.isPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public SneakerDetailsServiceModel Details(int id)
            => this.data
            .Sneakers
            .Where(s => s.Id == id)
            .ProjectTo<SneakerDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        private IEnumerable<SneakerServiceModel> GetSneakers(IQueryable<Sneaker> sneakersQuery)
            => sneakersQuery
            .ProjectTo<SneakerServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();

        public void ChangeVisibility(int sneakerId)
        {
            var sneaker = this.data.Sneakers.Find(sneakerId);

            sneaker.isPublic = !sneaker.isPublic;

            this.data.SaveChanges();
        }

        public IEnumerable<LatestSneakerServiceModel> Latest()
            => this.data
                .Sneakers
            .Where(s => s.isPublic)
                .OrderByDescending(s => s.Id)
                .ProjectTo<LatestSneakerServiceModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();
    }
}
