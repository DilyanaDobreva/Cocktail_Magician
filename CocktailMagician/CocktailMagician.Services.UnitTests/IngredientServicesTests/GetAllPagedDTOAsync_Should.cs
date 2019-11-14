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
    public class GetAllPagedDTOAsync_Should
    {
        [TestMethod]
        public async Task ReturnFullPageNotDeletedIngredients()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredient1Name = "Name1";
            var ingredient2Name = "Name2";
            var ingredient3Name = "Name3";

            var deletedIngredientName = "Deleted";
            var ingredientUnit = "Unit";

            var ingredient1 = new Ingredient
            {
                Name = ingredient1Name,
                Unit = ingredientUnit
            };
            var ingredient2 = new Ingredient
            {
                Name = ingredient2Name,
                Unit = ingredientUnit
            };
            var ingredient3 = new Ingredient
            {
                Name = ingredient3Name,
                Unit = ingredientUnit
            };
            var deletedIngredient = new Ingredient
            {
                Name = deletedIngredientName,
                Unit = ingredientUnit,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ReturnFullPageNotDeletedIngredients));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.AddRange(new List<Ingredient> { ingredient1, ingredient2, ingredient3, deletedIngredient });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                var listOfIngredients = await sut.GetAllPagedDTOAsync(2, 1);

                Assert.AreEqual(2, listOfIngredients.Count());
                Assert.IsFalse(listOfIngredients.Any(i => i.Name == deletedIngredientName));
            }
        }
        [TestMethod]
        public async Task ReturnLastPageNotDeletedIngredients()
        {
            var ingredientFactoryMock = new Mock<IIngredientFactory>();

            var ingredient1Name = "Name1";
            var ingredient2Name = "Name2";
            var ingredient3Name = "Name3";

            var deletedIngredientName = "Deleted";
            var ingredientUnit = "Unit";

            var ingredient1 = new Ingredient
            {
                Name = ingredient1Name,
                Unit = ingredientUnit
            };
            var ingredient2 = new Ingredient
            {
                Name = ingredient2Name,
                Unit = ingredientUnit
            };
            var ingredient3 = new Ingredient
            {
                Name = ingredient3Name,
                Unit = ingredientUnit
            };
            var deletedIngredient = new Ingredient
            {
                Name = deletedIngredientName,
                Unit = ingredientUnit,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ReturnLastPageNotDeletedIngredients));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.AddRange(new List<Ingredient> { ingredient1, ingredient2, ingredient3, deletedIngredient });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new IngredientServices(assertContext, ingredientFactoryMock.Object);
                var listOfIngredients = await sut.GetAllPagedDTOAsync(2, 2);

                Assert.AreEqual(1, listOfIngredients.Count());
                Assert.IsFalse(listOfIngredients.Any(i => i.Name == deletedIngredientName));
            }
        }
    }
}
