namespace SneakersShop.Data.Models
{
    
    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Sneakers> Sneakers { get; init; } = new List<Sneakers>();
    }
}
