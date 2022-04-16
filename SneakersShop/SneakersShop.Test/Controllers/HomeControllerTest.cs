using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SneakersShop.Controllers;
using SneakersShop.Test.Mocks;

namespace SneakersShop.Test.Controllers
{
    public class HomeControllerTest
    {
        [Test]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null,null,null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
