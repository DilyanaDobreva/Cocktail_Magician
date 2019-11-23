using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.IngredientServicesTests
{
    [TestClass]
    public class AddAsync_Should
    {
        [TestMethod]
        public async Task ThrowsException_WhenIngredientAlreadyExists()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit
            };

            ingredientFactoryMock
                .Setup(f => f.Create(ingredientName, ingredientUnit))
                .Returns(ingredient);

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIngredientAlreadyExists));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.Add(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddAsync(ingredientName, ingredientUnit));
            }

        }
        [TestMethod]
        public async Task AddIngredientToDatabase()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit
            };

            ingredientFactoryMock
                .Setup(f => f.Create(ingredientName, ingredientUnit))
                .Returns(ingredient);

            var options = TestUtilities.GetOptions(nameof(AddIngredientToDatabase));

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                var ingredientInDB = await sut.AddAsync(ingredientName, ingredientUnit);

                Assert.IsTrue(await assertContext.Ingredients.AnyAsync(i => i.Name == ingredientName && i.Unit == ingredientUnit));
                Assert.AreEqual(1, assertContext.Ingredients.Count());
                Assert.IsTrue(ingredientInDB.Name == ingredientName && ingredient.Unit == ingredientUnit);
            }
        }
    }
}
