using SneakersShop.Data.Models;
using SneakersShop.Tests.Mocks;
using Xunit;

namespace SneakersShop.Tests.Controllers
{
    public class SneakersControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectData()
        {
            //SneakerQueryServiceModel
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            //var sneakerService = SneakerServiceMock.Instance;

            data.Sneakers.Add(new Sneaker
            {
                Brand = "Nike",
                Model = "Air Max 95",
                Color = "Blue",
                Description = "just something",
                ImageUrl = "some link for nike"
            });
            data.SaveChanges();
        }
    }
}
