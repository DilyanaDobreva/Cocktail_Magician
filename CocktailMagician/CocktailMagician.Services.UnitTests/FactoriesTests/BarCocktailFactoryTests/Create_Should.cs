using Microsoft.VisualStudio.TestTools.UnitTesting;
using CocktailMagician.Services.Factories;
using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.BarCocktailFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfBarCocktail()
        {
            var barId = 1;
            var cocktailId = 1;

            var sut = new BarCocktailFactory();
            var barCocktailTest = sut.Create(barId, cocktailId);

            Assert.IsInstanceOfType(barCocktailTest, typeof(BarCocktail));
            Assert.AreEqual(barId, barCocktailTest.BarId);
            Assert.AreEqual(cocktailId, barCocktailTest.CocktailId);
        }
    }
}
