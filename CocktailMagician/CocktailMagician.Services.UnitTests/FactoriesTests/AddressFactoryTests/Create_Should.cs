using CocktailMagician.Data.Models;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.AddressFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfAddress()
        {
            var name = "Name";
            var cityId = 1;
            var latitude = 1.0;
            var longitude = 1.0;

            var sut = new AddressFactory();

            var addressTest = sut.Create(name, cityId, latitude, longitude);

            Assert.IsInstanceOfType(addressTest, typeof(Address));
            Assert.AreEqual(name, addressTest.Name);
            Assert.AreEqual(cityId, addressTest.CityId);
            Assert.AreEqual(latitude, addressTest.Latitude);
            Assert.AreEqual(longitude, addressTest.Longitude);
        }
    }
}
