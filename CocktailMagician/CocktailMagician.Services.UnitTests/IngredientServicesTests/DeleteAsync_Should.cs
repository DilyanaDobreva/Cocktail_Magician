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
    public class DeleteAsync_Should
    {
        [TestMethod]
        public async Task ThrowsException_WhenIdIsInvalid()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var invalidId = 100;

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.Add(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteAsync(invalidId));

            }
        }
        [TestMethod]
        public async Task ThrowsException_WhenIngredientIsDeleted()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIngredientIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.Add(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var ingredientId = await assertContext.Ingredients.Where(i => i.Name == ingredientName).Select(i => i.Id).FirstAsync();

                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteAsync(ingredientId));
            }
        }
        [TestMethod]
        public async Task ThrowsException_WhenIngredientIsPartOfCocktail()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit,
            };

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
            };

            var ingredientQuantity = 0.5;

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIngredientIsPartOfCocktail));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = ingredientQuantity
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var ingredientId = await assertContext.Ingredients.Where(i => i.Name == ingredientName).Select(i => i.Id).FirstAsync();

                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteAsync(ingredientId));
            }
        }
        [TestMethod]
        public async Task SetIngredientAsDeleted()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredientName = "Name";
            var ingredientUnit = "Unit";

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingredientUnit
            };

            var options = TestUtilities.GetOptions(nameof(SetIngredientAsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.Add(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var ingredientId = await actContext.Ingredients.Where(i => i.Name == ingredientName).Select(i => i.Id).FirstAsync();
                var sut = new IngredientServices(actContext, ingredientFactoryMock.Object);
                await sut.DeleteAsync(ingredientId);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var ingredientInDB = await assertContext.Ingredients.FirstAsync(i => i.Name == ingredientName);

                Assert.IsTrue(ingredientInDB.IsDeleted);
            }
        }
    }
}
