namespace SneakersShop.Services.Sneakers.Models
{
    public class SneakerQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int SneakersPerPage { get; init; }

        public int TotalSneakers { get; set; }

        public IEnumerable<SneakerServiceModel> Sneakers { get; init; }
    }
}
