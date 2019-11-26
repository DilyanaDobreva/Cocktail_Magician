using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class DeleteAsync_Should
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteAsync(invalidId));
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteAsync(barId));
            };
        }
        [TestMethod]
        public async Task SetBArAsDeletedAndDeletesItsBarCosktails()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";
            var cocktail1TestName = "Cocktail1Test";
            var cocktail2TestName = "Cocktail2Test";

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

            var cocktail1Test = new Cocktail
            {
                Name = cocktail1TestName,
                ImagePath = imagaUrlTest,
            };

            var cocktail2Test = new Cocktail
            {
                Name = cocktail2TestName,
                ImagePath = imagaUrlTest,
            };

            var options = TestUtilities.GetOptions(nameof(SetBArAsDeletedAndDeletesItsBarCosktails));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();

                var bar = await arrangeContext.Bars.FirstOrDefaultAsync(b => b.Name == barTestName);

                arrangeContext.BarCocktails.Add(new BarCocktail { Bar = bar, Cocktail = cocktail1Test });
                arrangeContext.BarCocktails.Add(new BarCocktail { Bar = bar, Cocktail = cocktail2Test });
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var barId = await actContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.DeleteAsync(barId);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var bar = await assertContext.Bars.FirstAsync(b => b.Name == barTestName);

                Assert.IsTrue(bar.IsDeleted);
                Assert.IsFalse(assertContext.BarCocktails.Any(bc => bc.BarId == bar.Id));
            }
        }
    }
}
