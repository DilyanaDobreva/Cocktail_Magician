using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class GetMostPopular_Should
    {
        [TestMethod]
        public async Task ReturnBars_WithHighestAverageRating()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName1 = "NameTest1";
            var barTestName2 = "NameTest2";
            var barTestName3 = "NameTest3";
            var barTestName4 = "NameTest4";

            var user = new User
            {
                Role = new Role { Name = "user" },
                UserName = "TestUser",
                Password = "Pass"
            };

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "TestCityName" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar1 = new Bar
            {
                Name = barTestName1,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };
            var bar2 = new Bar
            {
                Name = barTestName2,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };
            var bar3 = new Bar
            {
                Name = barTestName3,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
                IsDeleted = true
            };
            var bar4 = new Bar
            {
                Name = barTestName4,
                ImageUrl = imagaUrlTest,
                Address = addressTest,
            };

            var allRatings = new List<BarReview>
            {
                new BarReview
                {
                    Bar = bar1,
                    Rating = 5,
                    User = user
                },
                new BarReview
                {
                    Bar = bar1,
                    Rating = 4,
                    User = user
                },
                new BarReview
                {
                    Bar = bar2,
                    Rating = 4,
                    User = user
                },
                new BarReview
                {
                    Bar = bar3,
                    Rating = 5,
                    User = user
                },
                new BarReview
                {
                    Bar = bar4,
                    Rating = 3,
                    User = user
                },
            };

            var options = TestUtilities.GetOptions(nameof(ReturnBars_WithHighestAverageRating));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.BarReviews.AddRange(allRatings);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var mostPopular = await sut.GetMostPopular(2);

                Assert.AreEqual(2, mostPopular.Count());
                Assert.IsFalse(mostPopular.Any(r => r.AverageRating == 3));
                Assert.IsFalse(mostPopular.Any(r => r.AverageRating == 5));
            }
        }
    }
}
