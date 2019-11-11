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
    public class LoginAsync_Should
    {
        [TestMethod]
        public async Task LoginAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(LoginAsync));

            var bann = new Bann();
            var role = new Role();

            var user = new User(userName, hashedPassword, roleId);
            hasherMock
                .Setup(h => h.Hasher(userPassword))
                .Returns(hashedPassword);

            user.Bann = bann;
            user.Role = role;


            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                var userTest = await sut.LoginAsync(userName,userPassword);

                Assert.AreEqual(userTest.UserName, user.UserName);
                Assert.AreEqual(userTest.Password, user.Password);
                Assert.AreEqual(userTest.RoleName, user.Role.Name);
            }
        }
        [TestMethod]
        public async Task ThrowsExceptionWhen_UserNotFound()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var bann = new Bann();
            var role = new Role();

            var user = new User(userName, userPassword, roleId);

            user.Bann = bann;
            user.Role = role;

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_UserNotFound));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.LoginAsync(userName, userPassword));
            };
        }
        [TestMethod]
        public async Task ThrowsExceptionWhen_UserIsBanned()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 1;
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_UserIsBanned));

            var bann = new Bann();
            var role = new Role();

            var user = new User(userName, hashedPassword, roleId);
            hasherMock
                .Setup(h => h.Hasher(userPassword))
                .Returns(hashedPassword);

            

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                user.Bann = bann;
                user.Role = role;
                user.IsDeleted = true;
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.LoginAsync(userName, userPassword));
            };
        }
    }
}
