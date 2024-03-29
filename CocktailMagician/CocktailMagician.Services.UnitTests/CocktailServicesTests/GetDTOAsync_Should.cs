﻿using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CocktailServicesTests
{
    [TestClass]
    public class GetDTOAsync_Should
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

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetDetailedDTOAsync(invalidId));
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

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowsExceptionWhen_CocktailIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cocktails.Add(cocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetDetailedDTOAsync(cocktailId));
            };
        }

        [TestMethod]
        public async Task ReturnsCocktail()
        {
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
                    Address = addressTest
                };


                var options = TestUtilities.GetOptions(nameof(ReturnsCocktail));

                using (var arrangeContext = new CocktailMagicianDb(options))
                {
                    arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredient,
                        Quatity = quantityTest
                    });

                    arrangeContext.BarCocktails.Add(new BarCocktail
                    {
                        Bar = bar,
                        Cocktail = cocktail
                    });
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new CocktailMagicianDb(options))
                {
                    var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                    var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                    var foundCocktail = await sut.GetDetailedDTOAsync(cocktailId);

                    Assert.AreEqual(cocktailNameTest, foundCocktail.Name);
                    Assert.AreEqual(1, foundCocktail.Ingredients.Count());
                    Assert.AreEqual(1, foundCocktail.Bars.Count());
                    Assert.IsInstanceOfType(foundCocktail, typeof(CocktailDetailsDTO));
                }
            }
        }
    }
}
