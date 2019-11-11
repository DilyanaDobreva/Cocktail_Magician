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
    public class GetDetailedDTOAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenIdIsInvalid()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var invalidId = 10;

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
                Address = addressTest
            };

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetDetailedDTOAsync(invalidId));
            };
        }

        [TestMethod]
        public async Task ThrowsExceptionWhen_BarIsDeleted()
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
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barId = await assertContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetDetailedDTOAsync(barId));
            };
        }

        [TestMethod]
        public async Task ReturnExactCocktail()
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

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barId = await assertContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var foundBar = await sut.GetDetailedDTOAsync(barId);

                Assert.IsInstanceOfType(foundBar, typeof(BarDetailsDTO));
                Assert.AreEqual(barTestName, foundBar.Name);
            };

        }
    }
}
