using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants;

namespace SneakersShop.Data.Models
{
    public class Sneakers
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(SneakersBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(SneakersModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(SneakersColorMaxLength)]
        public string Color { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; init; }
    }
}
