using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.CocktailIngredientFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfCocktailIngredient_WithCocktailAsParameter()
        {
            var cocktail = new Cocktail
            {
                Name = "Name",
                ImageUrl = "www.test.com"
            };

            var ingredientId = 1;
            var quantity = 1.0;

            var sut = new CocktailIngredientFactory();
            var cocktailIngredient = sut.Create(cocktail, ingredientId, quantity);

            Assert.IsInstanceOfType(cocktailIngredient, typeof(CocktailIngredient));
            Assert.AreEqual("Name", cocktailIngredient.Cocktail.Name);
            Assert.AreEqual(ingredientId, cocktailIngredient.IngredientId);
            Assert.AreEqual(quantity, cocktailIngredient.Quatity);
        }
        [TestMethod]
        public void ReturnInstanceOfCocktailIngredient_WithCocktailIdAsParameter()
        {
            var cocktailId = 1;
            var ingredientId = 1;
            var quantity = 1.0;

            var sut = new CocktailIngredientFactory();
            var cocktailIngredient = sut.Create(cocktailId, ingredientId, quantity);

            Assert.IsInstanceOfType(cocktailIngredient, typeof(CocktailIngredient));
            Assert.AreEqual(cocktailId, cocktailIngredient.CocktailId);
            Assert.AreEqual(ingredientId, cocktailIngredient.IngredientId);
            Assert.AreEqual(quantity, cocktailIngredient.Quatity);
        }
    }
}
