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
    public class DeleteUserAsync_Should
    {
        [TestMethod]
        public async Task DeleteUserAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(DeleteUserAsync));

            var bann = new Bann();
            var role = new Role();
            var user = new User(userName, userPassword, roleId);

            using (var actContext = new CocktailMagicianDb(options))
            {
                user.Bann = bann;
                user.Role = role;
                actContext.Users.Add(user);
                await actContext.SaveChangesAsync();
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.DeleteUserAsync(user.Id);
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var userTest = await assertContext.Users.FirstAsync(u => u.UserName == userName);

                Assert.IsTrue(userTest.IsDeleted);
            }
        }
        [TestMethod]
        public async Task DeleteUserAsync_WhenUserBanned()
        {
            var userName = "user";
            var userPassword = "user";
            var bannReason = "Just Banned";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(DeleteUserAsync_WhenUserBanned));

            
            var user = new User(userName, userPassword, roleId);
            var bann = new Bann(bannReason, DateTime.MaxValue, user);
            var role = new Role();

            using (var actContext = new CocktailMagicianDb(options))
            {
                user.Bann = bann;
                user.Role = role;
                bann.User = user;
                actContext.Users.Add(user);
                actContext.Banns.Add(bann);
                await actContext.SaveChangesAsync();
                user.BannId = bann.Id;

                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.DeleteUserAsync(user.Id);
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var userTest = await assertContext.Users.FirstAsync(u => u.UserName == userName);

                Assert.IsTrue(userTest.IsDeleted);
                Assert.IsTrue(bann.IsDeleted);
            }
        }
    }
}
