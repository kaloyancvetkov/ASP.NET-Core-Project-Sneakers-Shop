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
        public void IsBySellerShouldWorkProperly()
        {
            //Arrange
            using var data = DatabaseMock.Instance;


            var seller = new Seller { Id = 1, UserId = "1", Name = "Pesho", PhoneNumber = "052644" };
            var sneaker = new Sneaker
            {
                Id = 33,
                Brand = "Nike",
                Model = "Air Max 2090",
                Color = "Yellow",
                Description = "just something",
                ImageUrl = "blablablabla",
                Price = 250,
                Seller = seller
            };
            data.Sneakers.Add(sneaker);
            data.SaveChanges();

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.IsBySeller(sneaker.Id, seller.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public void CategoryExistsShouldWorkProperly()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            var category = new Category { Id = 45, Name = "Lifestyle" };
            data.Categories.Add(category);
            data.SaveChanges();

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.CategoryExists(category.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateShouldReturnCorrectIdWhenCreated()
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
                CategoryId = 2,
                SellerId = 4
            };

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.Create(sneaker.Brand, 
                sneaker.Model, 
                sneaker.Color, 
                sneaker.Description, 
                sneaker.ImageUrl, 
                sneaker.Price, 
                sneaker.CategoryId, 
                sneaker.SellerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void EditShouldReturnTrueWhenEdited()
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
                CategoryId = 2,
                SellerId = 4
            };
            data.Sneakers.Add(sneaker);
            data.SaveChanges();

            var sneakerService = new SneakerService(data, null, null);

            //Act
            var result = sneakerService.Edit(
                1,
                "Adidas",
                "Yeezy",
                "Blue",
                "Some long descr",
                "Some link from net",
                330,
                4,
                true);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
