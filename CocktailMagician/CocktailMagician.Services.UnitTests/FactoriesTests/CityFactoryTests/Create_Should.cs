using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.CityFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfCity()
        {
            var name = "Name";

            var sut = new CityFactory();
            var city = sut.Create(name);

            Assert.IsInstanceOfType(city, typeof(City));
            Assert.AreEqual(name, city.Name);
        }
    }
}
