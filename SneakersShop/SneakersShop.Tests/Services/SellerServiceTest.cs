using SneakersShop.Data;
using SneakersShop.Data.Models;
using SneakersShop.Services.Sellers;
using SneakersShop.Tests.Mocks;
using Xunit;

namespace SneakersShop.Tests.Services
{
    public class SellerServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsSellerShouldReturnTrueIfUserIsSeller()
        {
            //Arrange
            using var data = GetSellerData();

            var sellerService = new SellerService(data);

            //Act
            var result = sellerService.IsSeller(UserId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSellerShouldReturnFalseIfUserIsNotSeller()
        {
            //Arrange
            using var data = GetSellerData();

            var sellerService = new SellerService(data);

            //Act
            var result = sellerService.IsSeller("Another");

            //Assert
            Assert.False(result);
        }

        private SneakersShopDbContext GetSellerData()
        {
            var data = DatabaseMock.Instance;

            data.Sellers.Add(new Seller
            {
                UserId = UserId,
                PhoneNumber = "0882",
                Name = "Ivan"
            });
            data.SaveChanges();

            return data;
        }
    }
}
