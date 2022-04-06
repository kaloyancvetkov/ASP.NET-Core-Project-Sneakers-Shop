using SneakersShop.Controllers.Api;
using SneakersShop.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SneakersShop.Tests.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalSneakers);
            Assert.Equal(10, result.TotalPurchases);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
