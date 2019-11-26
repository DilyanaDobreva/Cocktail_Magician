using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.CocktailFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfCocktail()
        {
            var name = "Name";
            var imageUrl = "www.test.com";

            var sut = new CocktailFactory();
            var cocktail = sut.Create(name, imageUrl);

            Assert.IsInstanceOfType(cocktail, typeof(Cocktail));
            Assert.AreEqual(name, cocktail.Name);
            Assert.AreEqual(imageUrl, cocktail.ImagePath);
        }
    }
}
