using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class FindUserAsync_Should
    {
        [TestMethod]
        public async Task FindUserAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(FindUserAsync));

            var user = new User(userName, userPassword, roleId);

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);

                var userTest = await sut.FindUserAsync(user.UserName);
                
                Assert.AreEqual(userName, userTest.UserName);
                Assert.AreEqual(userPassword, userTest.Password);
            }
        }
    }
}
