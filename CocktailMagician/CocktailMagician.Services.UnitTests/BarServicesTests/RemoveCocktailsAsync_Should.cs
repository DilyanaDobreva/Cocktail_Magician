using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class RemoveCocktailsAsync_Should
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.RemoveCocktailsAsync(invalidId, new List<int>()));
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.RemoveCocktailsAsync(barId, new List<int>()));
            };
        }

        [TestMethod]
        public async Task RemoveSelectedCocktails()
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
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };

            var cocktail1Test = new Cocktail
            {
                Name = cocktail1TestName,
                ImageUrl = imagaUrlTest,
            };

            var cocktail2Test = new Cocktail
            {
                Name = cocktail2TestName,
                ImageUrl = imagaUrlTest,
            };

            var options = TestUtilities.GetOptions(nameof(RemoveSelectedCocktails));

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
                var cocktail1Id = await actContext.Cocktails.Where(c => c.Name == cocktail1TestName).Select(c => c.Id).FirstAsync();

                var listOfCocktails = new List<int> { cocktail1Id };

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.RemoveCocktailsAsync(barId, listOfCocktails);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var bar = await assertContext.Bars
                    .Include(b => b.BarCocktails)
                    .ThenInclude(bc => bc.Cocktail)
                    .Where(b => b.Name == barTestName)
                    .FirstAsync();

                Assert.AreEqual(1, bar.BarCocktails.Count());
                Assert.IsTrue(!bar.BarCocktails.Any(c => c.Cocktail.Name == cocktail1TestName));
            }
        }

        [TestMethod]
        public async Task RemovesCocktailsWithValidIds()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";
            var cocktail1TestName = "Cocktail1Test";
            var cocktail2TestName = "Cocktail2Test";

            var invalidCocktailId = 10;

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

            var cocktail1Test = new Cocktail
            {
                Name = cocktail1TestName,
                ImageUrl = imagaUrlTest,
            };

            var cocktail2Test = new Cocktail
            {
                Name = cocktail2TestName,
                ImageUrl = imagaUrlTest,
            };


            var options = TestUtilities.GetOptions(nameof(RemovesCocktailsWithValidIds));

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
                var cocktail1Id = await actContext.Cocktails.Where(c => c.Name == cocktail1TestName).Select(c => c.Id).FirstAsync();

                var listOfCocktails = new List<int> { invalidCocktailId, cocktail1Id };

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.RemoveCocktailsAsync(barId, listOfCocktails);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var bar = await assertContext.Bars
                    .Include(b => b.BarCocktails)
                    .ThenInclude(bc => bc.Cocktail)
                    .Where(b => b.Name == barTestName)
                    .FirstAsync();

                Assert.AreEqual(1, bar.BarCocktails.Count());
                Assert.IsTrue(bar.BarCocktails.Any(c => c.Cocktail.Name == cocktail2TestName));
            }

        }
    }
}
