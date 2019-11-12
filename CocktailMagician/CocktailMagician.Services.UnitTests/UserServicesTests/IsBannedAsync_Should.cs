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
    public class IsBannedAsync_Should
    {
        [TestMethod]
        public async Task IsBannedAsync_True()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 1;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(IsBannedAsync_True));

            var user = new User(userName, userPassword, roleId);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {

                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                var isBanned = await sut.IsBannedAsync(user.UserName);

                Assert.AreEqual(false, isBanned);
            }
        }
        [TestMethod]
        public async Task IsBannedAsync_False()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 1;
            var bannReason = "Just banned.";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(IsBannedAsync_False));

            var user = new User(userName, userPassword, roleId);
            var bann = new Bann(bannReason, DateTime.MaxValue, user);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                arrangeContext.Banns.Add(bann);
                bann.User = user;
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                var isBanned = await sut.IsBannedAsync(user.UserName);

                Assert.AreEqual(true, isBanned);
            }
        }
    }
}
