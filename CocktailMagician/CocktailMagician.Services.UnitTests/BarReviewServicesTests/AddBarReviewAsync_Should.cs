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
    public class AddBarReviewAsync_Should
    {
        [TestMethod]
        public async Task AddReviewAsync()
        {
            var comment = "comment";
            var rating = 5;
            var userName = "user";
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

          



            userServicesMock
                .Setup(r => r.FindUserAsync(userName))
                .ReturnsAsync(user);

            var options = TestUtilities.GetOptions(nameof(AddReviewAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var barFound = await actContext.Bars.Where(b => b.Name == barTest.Name).Select(b => b.Id).FirstAsync();
                var userNameFound = await actContext.Users.Where(u => u.UserName == userName).Select(u => u.UserName).FirstAsync();

                var userIdFound = await actContext.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();

                var barReview = new BarReview
                {
                    Comment = comment,
                    Rating = rating,
                    UserId = userIdFound,
                    BarId = barFound
                };

                barReviewFactoryMock
                    .Setup(r => r.Create(comment, rating, userIdFound, barFound))
                    .Returns(barReview);

                var sut = new BarReviewServices(barReviewFactoryMock.Object, actContext );
                await sut.AddReviewAsync(comment, rating, userNameFound, barFound);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barReviewTest = await assertContext.BarReviews.Include(b => b.User).FirstAsync(u => u.User == user);
                var userIdFound = await assertContext.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();

                Assert.AreEqual(barReviewTest.User.Id, user.Id);
            }
        }
    }
}
