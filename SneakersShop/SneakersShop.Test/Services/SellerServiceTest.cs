using NUnit.Framework;
using SneakersShop.Data.Models;
using SneakersShop.Services.Sellers;
using SneakersShop.Test.Mocks;

namespace SneakersShop.Test.Services
{
    public class SellerServiceTest
    {
        [Test]
        public void IsSellerShouldReturnTrueWhenDataIsValid()
        {
            //Arrange
            const string userId = "TestUserId";

            using var data = DatabaseMock.Instance;

            data.Sellers.Add(new Seller
            {
                Name = "Pesho",
                UserId = userId,
                PhoneNumber = "052678876"
            });
            data.SaveChanges();

            var sellerService = new SellerService(data);

            //Act
            var result = sellerService.IsSeller(userId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IdByUserShouldReturnProperIdIfSellerWithTheGivenIdExists()
        {
            //Arrange
            const string userId = "TestUserId";

            using var data = DatabaseMock.Instance;

            data.Sellers.Add(new Seller
            {
                Id = 2,
                Name = "Pesho",
                UserId = userId,
                PhoneNumber = "052678876"
            });
            data.SaveChanges();

            var sellerService = new SellerService(data);

            //Act
            var result = sellerService.IdByUser(userId);

            //Assert
            Assert.AreEqual(2, result);
        }
    }
}
