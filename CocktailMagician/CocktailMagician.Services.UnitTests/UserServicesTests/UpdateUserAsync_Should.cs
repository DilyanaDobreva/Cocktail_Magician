using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class UpdateUserAsync_Should
    {
        [TestMethod]
        public async Task UpdateUserAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var newUserPassword = "NewUser";
            var roleId = 2;
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(UpdateUserAsync));

            var user = new User(userName, hashedPassword, roleId);

            hasherMock
               .Setup(h => h.Hasher(userPassword))
               .Returns(hashedPassword);

            hasherMock
               .Setup(h => h.Hasher(newUserPassword))
               .Returns(newUserPassword);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.UpdateUserAsync(user.Id, userPassword, newUserPassword, roleId);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var updatedUserPassword = await assertContext.Users.Where(u => u.Id == user.Id).Select(u => u.Password).FirstAsync();

                Assert.AreEqual(newUserPassword, updatedUserPassword);
            }
        }
        [TestMethod]
        public async Task UpdateUserAsync_WhenRoleIs0()
        {
            var userName = "user";
            var userPassword = "user";
            var newUserPassword = "NewUser";
            var roleId = 0;
            var newRoleId = 1;
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(UpdateUserAsync_WhenRoleIs0));

            var user = new User(userName, hashedPassword, roleId);

            hasherMock
               .Setup(h => h.Hasher(userPassword))
               .Returns(hashedPassword);

            hasherMock
               .Setup(h => h.Hasher(newUserPassword))
               .Returns(newUserPassword);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.UpdateUserAsync(user.Id, userPassword, newUserPassword, roleId);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var userFind = await assertContext.Users.Where(u => u.Id == user.Id).Select(u => u.RoleId).FirstAsync();

                Assert.AreEqual(newRoleId, userFind);
            }
        }
        [TestMethod]
        public async Task UpdateUserAsync_UpdateRole()
        {
            var userName = "user";
            var userPassword = "user";
            var newUserPassword = "NewUser";
            var roleId = 2;
            var newRole = 1;
            var hashedPassword = "!HASHED!";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(UpdateUserAsync_UpdateRole));

            var user = new User(userName, hashedPassword, roleId);

            hasherMock
               .Setup(h => h.Hasher(userPassword))
               .Returns(hashedPassword);

            hasherMock
               .Setup(h => h.Hasher(newUserPassword))
               .Returns(newUserPassword);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.UpdateUserAsync(user.Id, userPassword, newUserPassword, newRole);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var updatedUserRoleId = await assertContext.Users.Where(u => u.Id == user.Id).Select(u => u.RoleId).FirstAsync();

                Assert.AreEqual(newRole, updatedUserRoleId);
            }
        }
        [TestMethod]
        public async Task ThrowsExceptionWhen_UpdateUser()
        {
            var userName = "user";
            var userPassword = "user";
            var wrongPassword = "wrong password";
            var newUserPassword = "newPassword";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var user = new User(userName, userPassword, roleId);

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_UpdateUser));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateUserAsync(user.Id,wrongPassword, newUserPassword, roleId));
            };
        }
    }
}
