using SneakersShop.Data.Common;

namespace SneakersShop.Data.Repositories
{
    public class SneakersShopDbRepository : Repository, ISneakersShopDbRepository
    {
        public SneakersShopDbRepository(SneakersShopDbContext context)
        {
            this.Context = context;
        }
    }
}
