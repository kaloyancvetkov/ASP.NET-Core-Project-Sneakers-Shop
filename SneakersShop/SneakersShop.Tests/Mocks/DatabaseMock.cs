using Microsoft.EntityFrameworkCore;
using SneakersShop.Data;
using System;

namespace SneakersShop.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static SneakersShopDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<SneakersShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new SneakersShopDbContext(dbContextOptions);
            }
        }
    }
}
