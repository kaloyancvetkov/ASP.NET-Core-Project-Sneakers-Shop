using Moq;
using SneakersShop.Services.Statistics;

namespace SneakersShop.Test.Mocks
{
    internal class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalPurchases = 10,
                        TotalSneakers = 20,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
