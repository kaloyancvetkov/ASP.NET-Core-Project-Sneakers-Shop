using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakersShop.Controllers;
using SneakersShop.Data.Models;
using SneakersShop.Models.Home;
using SneakersShop.Services.Sneakers;
using SneakersShop.Services.Statistics;
using SneakersShop.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SneakersShop.Tests.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Sneakers.Add(new Sneaker
            {
                Brand = "Nike",
                Model = "Air Max 95",
                Color = "Blue",
                Description = "just something",
                ImageUrl = "some link for nike"
            });
            data.Add(new User());
            data.SaveChanges();

            var sneakerService = new SneakerService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, sneakerService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(1, indexViewModel.Sneakers.Count);
            Assert.Equal(1, indexViewModel.TotalSneakers);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
