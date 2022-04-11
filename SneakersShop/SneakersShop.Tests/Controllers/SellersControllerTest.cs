using MyTested.AspNetCore.Mvc;
using SneakersShop.Controllers;
using Xunit;

namespace SneakersShop.Tests.Controllers
{
    public class SellersControllerTest
    {
        [Fact]
        public void BecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Sellers/Become")
            .To<SellersController>(s => s.Become())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
