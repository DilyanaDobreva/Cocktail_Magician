using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class AddAsync_Should
    {
        [TestMethod]
        public async Task ThrowsException_WhenBarNameIsAlreadyTaken()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var barTest = new Bar
            {
                Name = barTestName,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenBarNameIsAlreadyTaken));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddAsync(barTestName, imagaUrlTest, new AddressDTO()));
            }
        }
        [TestMethod]
        public async Task AddBarToDB()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";
            var cityTest = new City
            {
                Name = "SofiaTest"
            };

            var options = TestUtilities.GetOptions(nameof(AddBarToDB));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.Add(cityTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var cityId = await actContext.Cities.Where(c => c.Name == cityTest.Name).Select(c => c.Id).FirstAsync();

                var addressDTOTest = new AddressDTO
                {
                    Name = "AddressTest",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1
                };
                var addressTest = new Address
                {
                    Name = "AddressTest",
                    CityId = cityId,
                    Latitude = 1.1,
                    Longitude = 1.1
                };

                //var address = addressDTO.MapFromDTO()

                barFactoryMock
                    .Setup(b => b.Create(barTestName, imagaUrlTest, addressDTOTest))
                    .Returns(new Bar
                    {
                        Name = barTestName,
                        ImageUrl = imagaUrlTest,
                        Address = addressTest,
                    });

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.AddAsync(barTestName, imagaUrlTest, addressDTOTest);
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barTest = await assertContext.Bars.FirstOrDefaultAsync(b => b.Name == barTestName);

                Assert.IsNotNull(barTest);
                Assert.AreEqual(1, assertContext.Bars.Count());
            }
        }
    }
}
