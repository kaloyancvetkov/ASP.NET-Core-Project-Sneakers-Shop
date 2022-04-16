using NUnit.Framework;
using SneakersShop.Data.Models;
using SneakersShop.Services.Sneakers;
using SneakersShop.Test.Mocks;
using System.Collections.Generic;

namespace SneakersShop.Test.Services
{
    public class SneakerServiceTest
    {
        [Test]
        public void AllBrandsShouldReturnValidResult()
        {
            //Arrange
            List<string> brands = new List<string>();
            brands.Add("Nike");

            using var data = DatabaseMock.Instance;

            data.Sneakers.Add(new Sneaker
            {
                Brand = "Nike",
                Model = "Air Max 2090",
                Color = "Yellow",
                Description = "just something",
                ImageUrl = "blablablabla",
                Price = 250,
            });
            data.SaveChanges();

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.AllBrands();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(brands, result);
        }

        [Test]
        public void AllCategoriesShouldReturnValidData()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var sneaker = new Sneaker
            {
                Brand = "Nike",
                Model = "Air Max 2090",
                Color = "Yellow",
                Description = "just something",
                ImageUrl = "blablablabla",
                Price = 250,
                Seller = new Seller { UserId = "1", Name = "Pesho", PhoneNumber = "052644" }
            };

            data.Sneakers.Add(new Sneaker
            {
                Brand = "Nike",
                Model = "Air Max 2090",
                Color = "Yellow",
                Description = "just something",
                ImageUrl = "blablablabla",
                Price = 250,
                Seller = new Seller { UserId = "1", Name = "Pesho", PhoneNumber = "052644"}
            });
            data.SaveChanges();

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.ByUser("1");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(sneaker, result);
        }
    }
}
