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
            this.CreateMap<Category, SneakerCategoryServiceModel>();
            this.CreateMap<SneakerDetailsServiceModel, SneakerFormModel>();
            this.CreateMap<Sneaker, SneakerServiceModel>()
                .ForMember(s => s.CategoryName, cfg => cfg.MapFrom(s => s.Category.Name));
            this.CreateMap<Sneaker, LatestSneakerServiceModel>();
            this.CreateMap<Sneaker, SneakerDetailsServiceModel>()
                .ForMember(s => s.UserId, cfg => cfg.MapFrom(s => s.Seller.UserId))
                .ForMember(s => s.CategoryName, cfg => cfg.MapFrom(s => s.Category.Name));
        }
    }
}
