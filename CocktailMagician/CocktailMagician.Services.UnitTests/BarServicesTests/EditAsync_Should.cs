using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class EditAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenIdIsInvalid()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var invalidId = 10;

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var barTest = new Bar
            {
                Name = barTestName,
                ImagePath = imagaUrlTest,
                Address = addressTest
            };

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditAsync(new BarToEditDTO { Id = invalidId }));
            };
        }

        [TestMethod]
        public async Task ThrowsExceptionWhen_BarIsDeleted()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var barTest = new Bar
            {
                Name = barTestName,
                ImagePath = imagaUrlTest,
                Address = addressTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_BarIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barId = await assertContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditAsync(new BarToEditDTO { Id = barId }));
            };
        }
        [TestMethod]
        public async Task EditAllBarProperties()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var imagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";
            var barEditedName = "EditedName";
            var addressEditedName = "New Address";
            var editedCity = new City { Name = "SofiaEdited" };
            var editedUrl = "https://www.gmail.com/";
            var editedLatitude = 2.2;
            var editedLongitude = 2.2;

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var barTest = new Bar
            {
                Name = barTestName,
                ImagePath = imagaUrlTest,
                Address = addressTest,
            };

            var options = TestUtilities.GetOptions(nameof(EditAllBarProperties));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Bars.Add(barTest);
                arrangeContext.Cities.Add(editedCity);
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var barId = await actContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();
                var editedCityId = await actContext.Cities.Where(c => c.Name == editedCity.Name).Select(c => c.Id).FirstAsync();
                var editDTO = new BarToEditDTO
                {
                    Id = barId,
                    Name = barEditedName,
                    ImagePath = editedUrl,
                    Address = new AddressDTO
                    {
                        Name = addressEditedName,
                        Latitude = editedLatitude,
                        Longitude = editedLongitude,
                        CityId = editedCityId
                    }
                };

                var sut = new BarServices(actContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.EditAsync(editDTO);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var editedBar = await assertContext.Bars
                    .Include(b => b.Address)
                    .ThenInclude(a => a.City)
                    .FirstAsync(b => b.Name == barEditedName);

                var bar = await assertContext.Bars
                    .FirstOrDefaultAsync(b => b.Name == barTestName);

                Assert.AreEqual(barEditedName, editedBar.Name);
                Assert.AreEqual(editedUrl, editedBar.ImagePath);
                Assert.AreEqual(addressEditedName, editedBar.Address.Name);
                Assert.AreEqual(editedLatitude, editedBar.Address.Latitude);
                Assert.AreEqual(editedLongitude, editedBar.Address.Longitude);
                Assert.AreEqual(editedCity.Name, editedBar.Address.City.Name);
                Assert.IsNull(bar);
            }
        }
    }
}
