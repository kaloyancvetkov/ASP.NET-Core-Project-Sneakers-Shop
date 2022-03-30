using System.ComponentModel.DataAnnotations;
using static SneakersShop.Data.DataConstants.Category;

namespace SneakersShop.Data.Models
{
    
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Sneaker> Sneakers { get; init; } = new List<Sneaker>();
    }
}
