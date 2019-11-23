using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CocktailServicesTests
{
    [TestClass]
    public class GetMostPopular_Should
    {
        [TestMethod]
        public async Task ReturnCocktails_WithHighestAverageRating()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktail1NameTest = "TestName1";
            var cocktail2NameTest = "TestName2";
            var cocktail3NameTest = "TestName3";
            var cocktail4NameTest = "TestName4";


            var CocktailImageUrlTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";

            var user = new User
            {
                Role = new Role { Name = "user" },
                UserName = "TestUser",
                Password = "Pass"
            };

            var cocktail1 = new Cocktail
            {
                Name = cocktail1NameTest,
                ImageUrl = CocktailImageUrlTest
            };

            var cocktail2 = new Cocktail
            {
                Name = cocktail2NameTest,
                ImageUrl = CocktailImageUrlTest,
            };

            var cocktail3 = new Cocktail
            {
                Name = cocktail3NameTest,
                ImageUrl = CocktailImageUrlTest,
                IsDeleted = true
            };

            var cocktail4 = new Cocktail
            {
                Name = cocktail4NameTest,
                ImageUrl = CocktailImageUrlTest,
            };

            var allRatings = new List<CocktailReview>
            {
                new CocktailReview
                {
                    Cocktail = cocktail1,
                    Rating = 5,
                    User = user
                },
                new CocktailReview
                {
                    Cocktail = cocktail1,
                    Rating = 4,
                    User = user
                },
                new CocktailReview
                {
                    Cocktail = cocktail2,
                    Rating = 4,
                    User = user
                },
                new CocktailReview
                {
                    Cocktail = cocktail3,
                    Rating = 5,
                    User = user
                },
                new CocktailReview
                {
                    Cocktail = cocktail4,
                    Rating = 3,
                    User = user
                },

            };

            var options = TestUtilities.GetOptions(nameof(ReturnCocktails_WithHighestAverageRating));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailReviews.AddRange(allRatings);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(actContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                var mostPopular =await sut.GetMostPopular(2);

                Assert.AreEqual(2, mostPopular.Count());
                Assert.IsFalse(mostPopular.Any(r => r.AverageRating == 3));
                Assert.IsFalse(mostPopular.Any(r => r.AverageRating == 5));

            }
        }
    }
}
