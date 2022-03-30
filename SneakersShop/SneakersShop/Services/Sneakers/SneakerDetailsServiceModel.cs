﻿namespace SneakersShop.Services.Sneakers
{
    public class SneakerDetailsServiceModel : SneakerServiceModel
    {
        public string Description { get; init; }

        public int CategoryId { get; init; }

        public int SellerId { get; init; }

        public string SellerName { get; init; }

        public string UserId { get; init; }
    }
}
