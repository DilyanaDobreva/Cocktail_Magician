using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CocktailServicesTests
{
    [TestClass]
    public class AllCocktailsCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedCocktailsCount()
        {
            {
                var cocktailFactoryMock = new Mock<ICocktailFactory>();
                var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
                var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

                var cocktail1NameTest = "TestName1";
                var cocktail2NameTest = "TestName2";

                var CocktailImageUrlTest = "https://www.google.com/";

                var ingrNameTest = "IngrTest";
                var ingrUnitTest = "Unit";
                var quantityTest = 0.5;

                var ingredient = new Ingredient
                {
                    Name = ingrNameTest,
                    Unit = ingrUnitTest
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
                    IsDeleted = true
                };

                var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedCocktailsCount));

                using (var arrangeContext = new CocktailMagicianDb(options))
                {
                    arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient,
                        Quatity = quantityTest
                    });

                    arrangeContext.Cocktails.Add(cocktail2);
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new CocktailMagicianDb(options))
                {
                    var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                    var count = await sut.AllCocktailsCountAsync();

                    Assert.AreEqual(1, count);
                }
            }

        }
    }
}
