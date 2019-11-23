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
    public class AddReviewAsync_Should
    {
        [TestMethod]
        public async Task AddReviewAsync()
        {
            var comment = "comment";
            var rating = 5;
            var userName = "user";
            var userpassword = "user";
            var roleId = 1;
            var cocktailTestName = "New Cocktail";


            var userServicesMock = new Mock<IUserServices>();
            var cocktailReviewFactoryMock = new Mock<ICocktailReviewFactory>();


            var cocktailTest = new Cocktail()
            {
                Name = cocktailTestName,

            };

            var user = new User(userName, userpassword, roleId);

          



            userServicesMock
                .Setup(r => r.FindUserAsync(userName))
                .ReturnsAsync(user);

            var options = TestUtilities.GetOptions(nameof(AddReviewAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cocktails.Add(cocktailTest);
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var cocktailFound = await actContext.Cocktails.Where(b => b.Name == cocktailTestName).Select(b => b.Id).FirstAsync();
                var userNameFound = await actContext.Users.Where(u => u.UserName == userName).Select(u => u.UserName).FirstAsync();

                var userIdFound = await actContext.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();

                var cocktailReview = new CocktailReview
                {
                    Comment = comment,
                    Rating = rating,
                    UserId = userIdFound,
                    CocktailId = cocktailFound
                };

                cocktailReviewFactoryMock
                    .Setup(r => r.Create(comment, rating, userIdFound, cocktailFound))
                    .Returns(cocktailReview);

                var sut = new CocktailReviewServices(cocktailReviewFactoryMock.Object, actContext );
                await sut.AddReviewAsync(comment, rating, userNameFound, cocktailFound);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailReviewTest = await assertContext.CocktailReviews.Include(b => b.User).FirstAsync(u => u.User == user);
                var userIdFound = await assertContext.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();

                Assert.AreEqual(cocktailReviewTest.User.Id, user.Id);
            }
        }
    }
}
