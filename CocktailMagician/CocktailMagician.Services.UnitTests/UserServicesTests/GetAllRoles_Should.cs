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
    public class GetAllRoles_Should
    {
        [TestMethod]
        public async Task GetAllRoles()
        {
            var roleName1 = "user";
            var roleId1 = 1;
            var roleName2 = "admin";
            var roleId2 = 2;

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(GetAllRoles));

            var role1 = new Role();
            var role2 = new Role();

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                role1.Name = roleName1;
                role1.Id = roleId1;
                role2.Name = roleName2;
                role2.Id = roleId2;

                arrangeContext.Roles.Add(role1);
                arrangeContext.Roles.Add(role2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new UserServices(assertContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                var listTest = await sut.GetAllRoles();

                Assert.IsTrue(listTest.Count() == 2);
                Assert.IsTrue(assertContext.Roles.Count() == 2);
            }
        }
    }
}
