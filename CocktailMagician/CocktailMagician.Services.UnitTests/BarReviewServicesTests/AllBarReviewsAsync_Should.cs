using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class AllBarReviewsAsync_Should
    {
        [TestMethod]
        public async Task AllReviewsAsync()
        {
            var userName = "user";
            var userName2 = "user";
            var userpassword = "user";
            var roleId = 1;
            var barTestName = "New Bar";
            var imageUrlTest = "https://www.google.bg/";


            var userServicesMock = new Mock<IUserServices>();
            var barReviewFactoryMock = new Mock<IBarReviewFactory>();

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
                ImageUrl = imageUrlTest,
                Address = addressTest,
            };


            var user = new User(userName, userpassword, roleId);
            var user2 = new User(userName2, userpassword, roleId);
            var barReview1 = new BarReview
            {
                Bar = barTest,
                User = user
            };

            var barReview2 = new BarReview
            {
                Bar = barTest,
                User = user2
            };

            userServicesMock
                .Setup(r => r.FindUserAsync(userName))
                .ReturnsAsync(user);

            var options = TestUtilities.GetOptions(nameof(AllReviewsAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                arrangeContext.Users.Add(user);
                arrangeContext.Users.Add(user2);
                arrangeContext.BarReviews.Add(barReview1);
                arrangeContext.BarReviews.Add(barReview2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {


                var barFound = await actContext.Bars.Where(b => b.Name == barTest.Name).Select(b => b.Id).FirstAsync();
                var sut = new BarReviewServices(barReviewFactoryMock.Object, actContext );
                await sut.AllReviewsAsync(barFound);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                Assert.AreEqual(2, assertContext.BarReviews.Count());
            }
        }
    }
}
