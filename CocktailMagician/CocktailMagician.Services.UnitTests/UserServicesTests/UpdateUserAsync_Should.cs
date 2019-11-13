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
        //CHECK
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
    }
}
