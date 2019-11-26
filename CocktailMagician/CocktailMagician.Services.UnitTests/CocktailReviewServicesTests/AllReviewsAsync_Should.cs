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
    public class AllReviewsAsync_Should
    {
        [TestMethod]
        public async Task AllReviewsAsync()
        {
            var userName = "user";
            var userName2 = "user";
            var userpassword = "user";
            var roleId = 1;
            var cocktailName = "New Bar";
            var imageUrlTest = "https://www.google.bg/";


            var userServicesMock = new Mock<IUserServices>();
            var cocktailReviewMock = new Mock<ICocktailReviewFactory>();

            

            var cocktailTest = new Cocktail
            {
                Name = cocktailName,
                ImagePath = imageUrlTest
            };


            var user = new User(userName, userpassword, roleId);
            var user2 = new User(userName2, userpassword, roleId);
            var cocktailReview = new CocktailReview
            {
                Cocktail = cocktailTest,
                User = user
            };

            var cocktailReview2 = new CocktailReview
            {
                Cocktail = cocktailTest,
                User = user2
            };

            userServicesMock
                .Setup(r => r.FindUserAsync(userName))
                .ReturnsAsync(user);

            var options = TestUtilities.GetOptions(nameof(AllReviewsAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cocktails.Add(cocktailTest);
                arrangeContext.Users.Add(user);
                arrangeContext.Users.Add(user2);
                arrangeContext.CocktailReviews.Add(cocktailReview);
                arrangeContext.CocktailReviews.Add(cocktailReview2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {


                var cocktailTestFound = await actContext.Cocktails.Where(b => b.Name == cocktailTest.Name).Select(b => b.Id).FirstAsync();
                var sut = new CocktailReviewServices(cocktailReviewMock.Object, actContext );
                await sut.AllReviewsAsync(cocktailTestFound);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                Assert.AreEqual(2, assertContext.CocktailReviews.Count());
            }
        }
    }
}
