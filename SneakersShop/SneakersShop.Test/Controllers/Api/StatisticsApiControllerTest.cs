using NUnit.Framework;
using SneakersShop.Controllers.Api;
using SneakersShop.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersShop.Test.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Test]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(10, result.TotalPurchases);
            Assert.AreEqual(20, result.TotalSneakers);
            Assert.AreEqual(15, result.TotalUsers);
        }
    }
}
