using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests.User
{
    [TestClass]
    public class Register_Should
    {
        [TestMethod]
        public async Task RegisterAdminAsync()
        {
            var adminName = "Admin";
            var adminPassword = "Admin";
            var roleId = 2;
            var roleName = "admin";
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(RegisterAdminAsync));
            var role = new Role();

            role.Id = roleId;
            role.Name = roleName;

            var user = new Data.Models.User(adminName, adminPassword, roleId);
            hasherMock
                .Setup(h => h.Hasher(adminPassword))
                .Returns(hashedPassword);

            userFactoryMock
                .Setup(u => u.CreateUser(adminName, adminPassword, roleId))
                .Returns(user);

            user.Role = role;

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.RegisterAdminAsync(adminName, adminPassword);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                Assert.AreEqual(1, assertContext.Users.Count());
                var testAdmin = assertContext.Users.FirstOrDefault(u => u.UserName == adminName);
                Assert.IsNotNull(testAdmin);
                Assert.AreEqual(hashedPassword, testAdmin.Password);
            }
        }
    }
}
