namespace SneakersShop.Models.Sneakers
{
    public class SneakersListingViewModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string Color { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }

        public string Category { get; init; }
    }
}
