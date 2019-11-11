using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class GetAllDTOAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedBars()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName1 = "NameTest1";
            var barTestName2 = "NameTest2";

            var cityTest = new City
            {
                Name = "SofiaTest"
            };

            var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedBars));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.Add(cityTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var cityId = await actContext.Cities.Where(c => c.Name == cityTest.Name).Select(c => c.Id).FirstAsync();

                var address1DTOTest = new AddressDTO
                {
                    Name = "AddressTest1",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1
                };
                var addressTest1 = new Address
                {
                    Name = "AddressTest1",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1
                };
                var address2DTOTest = new AddressDTO
                {
                    Name = "AddressTest2",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1
                };
                var addressTest2 = new Address
                {
                    Name = "AddressTest2",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1


                };

                var bar1 = new Bar
                {
                    Name = barTestName1,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest1,
                };
                var bar2 = new Bar
                {
                    Name = barTestName2,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest1,
                    IsDeleted = true
                };

                actContext.Bars.Add(bar1);
                actContext.Bars.Add(bar2);
                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var listTest = await sut.GetAllDTOAsync(5, 1);

                Assert.IsTrue(listTest.Count() == 1);
                Assert.IsTrue(assertContext.Bars.Count() == 2);
            }

        }
    }
}
