namespace SneakersShop.Services.Sneakers.Models
{
    public class SneakerDetailsServiceModel : SneakerServiceModel
    {
        public string Description { get; init; }

        public int CategoryId { get; init; }

        public string CategoryName { get; init; }

        public int SellerId { get; init; }

        public string SellerName { get; init; }

        public string UserId { get; init; }
    }
}
