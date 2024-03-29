﻿using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CocktailServicesTests
{
    [TestClass]
    public class AddBarsAsync_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenIdIsInvalid()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var invalidId = 10;

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest
            };

            var barImagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar = new Bar
            {
                Name = barTestName,
                ImagePath = barImagaUrlTest,
                Address = addressTest
            };


            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.Bars.Add(bar);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var barId = await assertContext.Bars.Where(b => b.Name == barTestName).Select(b => b.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddBarsAsync(invalidId, new List<int> { barId }));
            };
        }
        [TestMethod]
        public async Task ThrowsExceptionWhen_CocktailIsDeleted()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
                IsDeleted = true
            };

            var barImagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar = new Bar
            {
                Name = barTestName,
                ImagePath = barImagaUrlTest,
                Address = addressTest
            };


            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_CocktailIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.Bars.Add(bar);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();
                var barId = await assertContext.Bars.Where(c => c.Name == barTestName).Select(c => c.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddBarsAsync(cocktailId, new List<int> { barId }));
            };
        }
        [TestMethod]
        public async Task ThrowsException_WhenBarIdIsInvalid()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var CocktailImageUrlTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var listWithInvalidId = new List<int> { 100 };

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = CocktailImageUrlTest
            };

            var barImagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar = new Bar
            {
                Name = barTestName,
                ImagePath = barImagaUrlTest,
                Address = addressTest
            };


            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenBarIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.Bars.Add(bar);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstOrDefaultAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddBarsAsync(cocktailId, listWithInvalidId));
            }
        }
        [TestMethod]
        public async Task ThrowsException_WhenBarIsDeleted()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var CocktailImageUrlTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = CocktailImageUrlTest
            };

            var barImagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar = new Bar
            {
                Name = barTestName,
                ImagePath = barImagaUrlTest,
                Address = addressTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenBarIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.Bars.Add(bar);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstOrDefaultAsync();
                var barId = await assertContext.Bars.Where(c => c.Name == barTestName).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddBarsAsync(cocktailId, new List<int> { barId }));
            }
        }
        [TestMethod]
        public async Task AddsBarToCocktailsList()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var CocktailImageUrlTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = CocktailImageUrlTest
            };

            var barImagaUrlTest = "https://www.google.com/";
            var barTestName = "NameTest";

            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar = new Bar
            {
                Name = barTestName,
                ImagePath = barImagaUrlTest,
                Address = addressTest,
            };

            var options = TestUtilities.GetOptions(nameof(AddsBarToCocktailsList));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.Bars.Add(bar);

                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await actContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstOrDefaultAsync();
                var barId = await actContext.Bars.Where(c => c.Name == barTestName).Select(c => c.Id).FirstAsync();

                barCocktailFactoryMock
                    .Setup(f => f.Create(barId, cocktailId))
                    .Returns(new BarCocktail { CocktailId = cocktailId, BarId = barId });

                var sut = new CocktailServices(actContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.AddBarsAsync(cocktailId, new List<int> { barId });
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailInDB = await assertContext.Cocktails
                    .Include(c => c.BarCocktails)
                    .FirstOrDefaultAsync(c => c.Name == cocktailNameTest);
                var barId = await assertContext.Bars.Where(c => c.Name == barTestName).Select(c => c.Id).FirstAsync();

                Assert.AreEqual(1, cocktailInDB.BarCocktails.Count());
                Assert.IsTrue(cocktailInDB.BarCocktails.Any(c => c.BarId == barId));
            }
        }
    }
}
