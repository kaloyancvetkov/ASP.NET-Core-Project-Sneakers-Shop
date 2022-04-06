using AutoMapper;
using SneakersShop.Infrastructure;

namespace SneakersShop.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
