using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CityServicesTests
{
    [TestClass]
    public class AddAsync_Should
    {
        [TestMethod]
        public async Task ThrowsException_WhenCityAlreadyExists()
        {
            var cityFactoryMock = new Mock<ICityFactory>();

            var cityName = "Name";

            var city = new City
            {
                Name = cityName
            };

            cityFactoryMock
                .Setup(f => f.Create(cityName))
                .Returns(city);

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenCityAlreadyExists));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.Add(city);
                await arrangeContext.SaveChangesAsync();
            }

                using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CityServices(assertContext, cityFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddAsync(cityName));
            }
        }

        [TestMethod]
        public async Task AddCityToDatabase()
        {
            var cityFactoryMock = new Mock<ICityFactory>();

            var cityName = "Name";

            var city = new City
            {
                Name = cityName
            };

            cityFactoryMock
                .Setup(f => f.Create(cityName))
                .Returns(city);

            var options = TestUtilities.GetOptions(nameof(AddCityToDatabase));

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CityServices(assertContext, cityFactoryMock.Object);
                var cityInDB = await sut.AddAsync(cityName);

                Assert.IsTrue(await assertContext.Cities.AnyAsync(i => i.Name == cityName));
                Assert.AreEqual(1, assertContext.Cities.Count());
                Assert.IsTrue(cityInDB.Name == cityName);
            }
        }
    }
}
