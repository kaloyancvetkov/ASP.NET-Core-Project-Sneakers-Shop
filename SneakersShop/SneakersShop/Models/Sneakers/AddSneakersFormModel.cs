using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants;


namespace SneakersShop.Models.Sneakers
{

    public class AddSneakersFormModel
    {
        [Required]
        [StringLength(SneakersBrandMaxLength, MinimumLength = SneakersBrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(SneakersModelMaxLength, MinimumLength = SneakersModelMinLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = SneakersDescriptionMinLength, ErrorMessage = "The field Description must be a string with a minimum length of {2}")]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [MinLength(SneakersColorMinLength)]
        [MaxLength(SneakersColorMaxLength)]
        public string Color { get; init; }

        [Range(0, 25000.99)]
        public decimal Price { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<SneakersCategoryViewModel> ?Categories { get; set; }
    }
}
