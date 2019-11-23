using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.ServiceTests
{
    [TestClass]
    public class BanAsync_Should
    {
        [TestMethod]
        public async Task BannAsync()
        {
            var userName = "user";
            var userPassword = "user";
            var roleId = 2;
            var reasonForBan = "Just banned";

            var userFactoryMock = new Mock<IUserFactory>();
            var bannFactoryMock = new Mock<IBannFactory>();
            var hasherMock = new Mock<IHasher>();

            var options = TestUtilities.GetOptions(nameof(BannAsync));

            
            var role = new Role();
            var user = new User(userName, userPassword, roleId);
            var bann = new Bann();
            var dateTime = DateTime.Today;

            bannFactoryMock
                .Setup(b => b.CreateBan(reasonForBan, dateTime.AddDays(30), user))
                .Returns(bann);

            using (var actContext = new CocktailMagicianDb(options))
            {
                user.Role = role;
                bann.User = user;
                bann.Reason = reasonForBan;
                actContext.Users.Add(user);
                await actContext.SaveChangesAsync();


                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName             
                };
                var sut = new UserServices(actContext, userFactoryMock.Object, bannFactoryMock.Object, hasherMock.Object);
                await sut.BanAsync(reasonForBan,userDTO);
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var banTest = await assertContext.Banns.Include(b => b.User).FirstAsync(u => u.User == user);

                Assert.AreEqual(banTest.User.Id, user.Id);
                Assert.AreEqual(banTest.Reason, bann.Reason);
            }
        }
    }
}
