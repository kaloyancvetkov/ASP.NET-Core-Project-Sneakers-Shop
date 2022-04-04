using AutoMapper;
using SneakersShop.Data.Models;
using SneakersShop.Models.Home;
using SneakersShop.Models.Sneakers;
using SneakersShop.Services.Sneakers.Models;

namespace SneakersShop.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<SneakerDetailsServiceModel, SneakerFormModel>();
            this.CreateMap<Sneaker, SneakersIndexViewModel>();
            this.CreateMap<Sneaker, SneakerDetailsServiceModel>()
                .ForMember(s => s.UserId, cfg => cfg.MapFrom(s => s.Seller.UserId));
        }
    }
}
