using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.IngredientFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfIngredient()
        {
            var name = "Name";
            var unit = "Unit";

            var sut = new IngredientFactory();
            var ingredient = sut.Create(name, unit);

            Assert.IsInstanceOfType(ingredient, typeof(Ingredient));
            Assert.AreEqual(name, ingredient.Name);
            Assert.AreEqual(unit, ingredient.Unit);
        }
    }
}
