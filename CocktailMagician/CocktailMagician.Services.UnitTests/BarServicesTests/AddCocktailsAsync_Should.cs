using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class AddCocktailsAsync_Should
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
                ImagePath = imagaUrlTest,
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddCocktailsAsync(invalidId, new List<int>()));
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
                ImagePath = imagaUrlTest,
                Address = addressTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_BarIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barId = await assertContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddCocktailsAsync(barId, new List<int>()));
            };
        }

        [TestMethod]
        public async Task AddCosktailToBarList()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";
            var cocktailTestName = "CocktailTest";

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
                ImagePath = imagaUrlTest,
                Address = addressTest,
            };

            var cocktailTest = new Cocktail
            {
                Name = cocktailTestName,
                ImagePath = imagaUrlTest,
            };

            var options = TestUtilities.GetOptions(nameof(AddCosktailToBarList));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {

                arrangeContext.Bars.Add(barTest);
                arrangeContext.Cocktails.Add(cocktailTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var barId = await actContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();
                var cocktailId = await actContext.Cocktails.Where(c => c.Name == cocktailTestName).Select(c => c.Id).FirstAsync();

                var listOfCocktails = new List<int> { cocktailId };

                barCocktailFactoryMock
                    .Setup(b => b.Create(barId, cocktailId))
                    .Returns(new BarCocktail
                    {
                        BarId = barId,
                        CocktailId = cocktailId
                    });

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.AddCocktailsAsync(barId, listOfCocktails);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var bar = await assertContext.Bars
                    .Include(b => b.BarCocktails)
                    .ThenInclude(bc => bc.Cocktail)
                    .Where(b => b.Name == barTestName)
                    .FirstAsync();

                Assert.AreEqual(1, bar.BarCocktails.Count());
                Assert.IsTrue(bar.BarCocktails.Any(c => c.Cocktail.Name == cocktailTestName));
            }
        }
    }
}
