using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class RegisterAdminAsync_Should
    {
        [TestMethod]
        public async Task RegisterAdminAsync()
        {
            var adminName = "user";
            var adminPassword = "user";
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

            var user = new User(adminName, adminPassword, roleId);
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
        [TestMethod]
        public async Task ThrowsExceptionWhen_RegisterAdminAsync()
        {
            var adminName = "user";
            var adminPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var user = new User(adminName, adminPassword, roleId);

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_RegisterAdminAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.RegisterAdminAsync(adminName, adminPassword));
            };
        }
    }
}
