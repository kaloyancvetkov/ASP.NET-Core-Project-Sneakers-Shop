using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants.Sneakers;

namespace SneakersShop.Data.Models
{
    public class Sneaker
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; init; }

        public int SellerId { get; init; }
        public Seller Seller { get; init; }
    }
}
