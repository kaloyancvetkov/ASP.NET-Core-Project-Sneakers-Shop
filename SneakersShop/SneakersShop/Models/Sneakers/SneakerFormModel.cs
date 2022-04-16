using SneakersShop.Services.Sneakers.Models;
using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants.Sneakers;

namespace SneakersShop.Models.Sneakers
{

    public class SneakerFormModel : ISneakerModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = DescriptionMinLength, ErrorMessage = "The field Description must be a string with a minimum length of {2}")]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [MinLength(ColorMinLength)]
        [MaxLength(ColorMaxLength)]
        public string Color { get; init; }

        [Range(0, 25000.99)]
        public decimal Price { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<SneakerCategoryServiceModel> ?Categories { get; set; }
    }
}
