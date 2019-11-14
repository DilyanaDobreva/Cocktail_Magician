using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.IngredientServicesTests
{
    [TestClass]
    public class AllIngredientsCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedIngredientsCount()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var deletedIngredientName = "Deleted";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit
            };
            var deleteIngredient = new Ingredient
            {
                Name = deletedIngredientName,
                Unit = ingredientUnit,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedIngredientsCount));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.AddRange(new List<Ingredient> { ingredient, deleteIngredient });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                var count = await sut.AllIngredientsCountAsync();

                Assert.AreEqual(1, count);
            }
        }
    }
}
