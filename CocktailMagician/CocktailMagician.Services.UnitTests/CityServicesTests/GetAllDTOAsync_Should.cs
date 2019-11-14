using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CityServicesTests
{
    [TestClass]
    public class GetAllDTOAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedCities()
        {
            var cityFactoryMock = new Mock<ICityFactory>();

            var cityName = "Name";
            var deletedCityName = "Deleted";

            var city = new City
            {
                Name = cityName
            };
            var deletedCity = new City
            {
                Name = deletedCityName,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedCities));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.AddRange(new List<City> { city, deletedCity });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CityServices(assertContext, cityFactoryMock.Object);
                var listOfCities = await sut.GetAllDTOAsync();

                Assert.AreEqual(1, listOfCities.Count());
                Assert.IsFalse(listOfCities.Any(c => c.Name == deletedCityName));
            }

        }
    }
}
