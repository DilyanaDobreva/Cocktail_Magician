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

namespace CocktailMagician.Services.UnitTests.IngredientServicesTests
{
    [TestClass]
    public class GetAllDTOAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedIngredients()
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

            var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedIngredients));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.AddRange(new List<Ingredient> { ingredient, deleteIngredient });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                var listOfIngredients = await sut.GetAllDTOAsync();

                Assert.AreEqual(1, listOfIngredients.Count());
                Assert.IsTrue(listOfIngredients.Any(i => i.Name == ingredientName));
            }
        }
    }
}
