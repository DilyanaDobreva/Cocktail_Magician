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
    public class RegisterUserAsync_Should
    {
        [TestMethod]
        public async Task RegisterUserAsync()
        {
            var userName = "user";
            var userpassword = "user";
            var roleId = 1;
            var roleName = "user";
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(RegisterUserAsync));
            var role = new Role();

            role.Id = roleId;
            role.Name = roleName;

            var user = new User(userName, userpassword, roleId);
            hasherMock
                .Setup(h => h.Hasher(userpassword))
                .Returns(hashedPassword);

            userFactoryMock
                .Setup(u => u.CreateUser(userName, userpassword, roleId))
                .Returns(user);

            user.Role = role;

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.RegisterUserAsync(userName, userpassword);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                Assert.AreEqual(1, assertContext.Users.Count());
                var testAdmin = assertContext.Users.FirstOrDefault(u => u.UserName == userName);
                Assert.IsNotNull(testAdmin);
                Assert.AreEqual(hashedPassword, testAdmin.Password);
            }
        }
        [TestMethod]
        public async Task ThrowsExceptionWhen_RegisterUserAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 1;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var user = new User(userName, userPassword, roleId);

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_RegisterUserAsync));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.RegisterUserAsync(userName,userPassword));
            };
        }
    }
}
