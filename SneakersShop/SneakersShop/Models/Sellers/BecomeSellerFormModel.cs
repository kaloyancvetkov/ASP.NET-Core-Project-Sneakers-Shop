using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants.Seller;

namespace SneakersShop.Models.Sellers
{
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
