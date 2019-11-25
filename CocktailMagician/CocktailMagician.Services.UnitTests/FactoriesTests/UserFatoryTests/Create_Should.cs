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

namespace CocktailMagician.Services.UnitTests.FactoriesTests.UserFactoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfType()
        {
            var userName = "Username";
            var userPassword = "User password";
            var role = 2;

            var sut = new UserFactory();
            var userFactory = sut.CreateUser(userName,userPassword,role);

            Assert.IsInstanceOfType(userFactory, typeof(User));
            Assert.AreEqual(userName, userFactory.UserName);
            Assert.AreEqual(userPassword, userFactory.Password);
        }
        [TestMethod]
        public void ThrowExpeption_WhenUserNameIsLessThan5Symbols()
        {
            var userName = "User";
            var userPassword = "User password";
            var role = 2;

            var sut = new UserFactory();
            var userFactory = 

            Assert.ThrowsException<ArgumentException>(() => sut.CreateUser(userName, userPassword, role));
        }
        [TestMethod]
        public void ThrowExpeption_WhenPasswordIsLessThan5Symbols()
        {
            var userName = "User Name";
            var userPassword = "User";
            var role = 2;

            var sut = new UserFactory();
            var userFactory =

            Assert.ThrowsException<ArgumentException>(() => sut.CreateUser(userName, userPassword, role));
        }
    }
}
