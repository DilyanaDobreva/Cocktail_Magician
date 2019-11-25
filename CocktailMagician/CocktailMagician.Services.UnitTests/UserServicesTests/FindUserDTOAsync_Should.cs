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
    public class FindUserDTOAsync_Should
    {
        [TestMethod]
        public async Task FindUserDTOAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(FindUserDTOAsync));

            var bann = new Bann();
            var role = new Role();
            var user = new User(userName, userPassword, roleId);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                user.Bann = bann;
                user.Role = role;
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                var userTest = await sut.FindUserDTOAsync(user.UserName);

                Assert.AreEqual(userTest.UserName, user.UserName);
                Assert.AreEqual(userTest.Password, user.Password);
                Assert.AreEqual(userTest.RoleName, user.Role.Name);
                Assert.AreEqual(userTest.BannId, user.BannId);
                Assert.AreEqual(userTest.BannReason, user.Bann.Reason);
                Assert.AreEqual(userTest.BannEndTime, user.Bann.EndDateTime);
            }
        }
        [TestMethod]
        public async Task ThrowException_WhenNameIsInvalid()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;
            var invalidName = "Invalid Name";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenNameIsInvalid));

            var bann = new Bann();
            var role = new Role();
            var user = new User(userName, userPassword, roleId);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                user.Bann = bann;
                user.Role = role;
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.FindUserDTOAsync(invalidName));
            }
        }
    }
}
