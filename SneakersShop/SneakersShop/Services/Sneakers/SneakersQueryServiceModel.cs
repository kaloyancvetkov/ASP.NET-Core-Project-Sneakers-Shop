namespace SneakersShop.Services.Sneakers
{
    public class SneakersQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int SneakersPerPage { get; init; }

        public int TotalSneakers { get; set; }

        public IEnumerable<SneakersServiceModel> Sneakers { get; init; }
    }
}
