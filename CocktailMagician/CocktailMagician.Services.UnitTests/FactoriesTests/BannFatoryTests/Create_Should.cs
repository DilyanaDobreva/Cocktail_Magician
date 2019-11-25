using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.BannFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfType()
        {
            var reason = "Just banned";

            var user = new User();

            var sut = new BannFactory();
            var bannFactory = sut.CreateBan(reason, DateTime.Now, user);

            Assert.IsInstanceOfType(bannFactory, typeof(Bann));
            Assert.AreEqual(reason, bannFactory.Reason);
        }
    }
}
