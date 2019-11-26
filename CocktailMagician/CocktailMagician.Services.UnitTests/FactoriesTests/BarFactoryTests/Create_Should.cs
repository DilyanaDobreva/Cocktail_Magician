using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.BarFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfType()
        {
            var name = "Bar";
            var imageUrl = "www.test.com";
            var phoneNum = "33333";

            var address = new AddressDTO
            {
                CityId = 1,
                Name = "Address",
                Latitude = 1.0,
                Longitude = 1.0,
            };

            var sut = new BarFactory();
            var bar = sut.Create(name, imageUrl, phoneNum, address);

            Assert.IsInstanceOfType(bar, typeof(Bar));
            Assert.AreEqual(name, bar.Name);
            Assert.AreEqual(imageUrl, bar.ImagePath);
            Assert.AreEqual(phoneNum, bar.PhoneNumber);            
        }
    }
}
