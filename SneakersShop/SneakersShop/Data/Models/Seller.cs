using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants.Seller;
namespace SneakersShop.Data.Models
{
    public class Seller
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Sneakers> Sneakers { get; init; } = new List<Sneakers>();
    }
}
