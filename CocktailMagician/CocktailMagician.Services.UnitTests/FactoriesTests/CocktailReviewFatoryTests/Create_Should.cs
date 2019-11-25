using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.CocktailReviewFatoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfType()
        {
            var comment = "New comment";
            var rating = 2;
            var userId = "1";
            var cocktailId = 1;

            var sut = new CocktailReviewFactory();
            var cockTailReview = sut.Create(comment, rating, userId, cocktailId);

            Assert.IsInstanceOfType(cockTailReview, typeof(CocktailReview));
            Assert.AreEqual(comment, cockTailReview.Comment);
            Assert.AreEqual(rating, cockTailReview.Rating);
            Assert.AreEqual(userId, cockTailReview.UserId);
            Assert.AreEqual(cocktailId, cockTailReview.CocktailId);

        }
    }
}
