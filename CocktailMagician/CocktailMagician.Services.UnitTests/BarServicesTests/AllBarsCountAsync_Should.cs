using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class AllBarsCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnNumberOfNotDeletedBars()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var bar1TestName = "Name1Test";
            var bar2TestName = "Name2Test";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar1Test = new Bar
            {
                Name = bar1TestName,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };

            var bar2Test = new Bar
            {
                Name = bar2TestName,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ReturnNumberOfNotDeletedBars));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(bar1Test);
                arrangeContext.Bars.Add(bar2Test);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var count = await sut.AllBarsCountAsync();

                Assert.AreEqual(1, count);
            }
        }
    }
}
