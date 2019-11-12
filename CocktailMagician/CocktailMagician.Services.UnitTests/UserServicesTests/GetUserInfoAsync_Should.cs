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
    public class GetUserInfoAsync_Should
    {
        [TestMethod]
        public async Task FinGetUserInfoAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(FinGetUserInfoAsync));

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
                var userTest = await sut.GetUserInfoAsync(user.Id);

                Assert.AreEqual(userTest.UserName, user.UserName);
                Assert.AreEqual(userTest.Password, user.Password);
                Assert.AreEqual(userTest.RoleName, user.Role.Name);
                Assert.AreEqual(userTest.IsDeleted, user.IsDeleted);
            }
        }
    }
}
