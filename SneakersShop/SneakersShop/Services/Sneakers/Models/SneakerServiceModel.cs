namespace SneakersShop.Services.Sneakers.Models
{
    public class SneakerServiceModel : ISneakerModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string Color { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }

        public string CategoryName { get; init; }

        public bool isPublic { get; init; }

    }
}
