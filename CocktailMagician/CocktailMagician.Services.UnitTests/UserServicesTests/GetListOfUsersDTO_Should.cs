using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class GetListOfUsersDTO_Should
    {
        [TestMethod]
        public async Task GetListOfUsersDTO()
        {
            var userName = "user";
            var userPassword = "user";
            var userName2 = "user2";
            var userPassword2 = "user2";
            var roleId = 1;
            var roleName = "user";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(GetListOfUsersDTO));

            var user = new User(userName, userPassword, roleId);
            var user2 = new User(userName2, userPassword2, roleId);

            var bann = new Bann();
            var role = new Role();

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                user.Role = role;
                user.Bann = bann;

                user2.Role = role;
                user2.Bann = bann;

                role.Name = roleName;
                role.Id = roleId;
                arrangeContext.Roles.Add(role);
                arrangeContext.Users.Add(user);
                arrangeContext.Users.Add(user2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                var listTest = await sut.GetListOfUsersDTO();

                Assert.IsTrue(listTest.Count() == 2);
                Assert.IsTrue(assertContext.Users.Count() == 2);
            }
        }
    }
}
