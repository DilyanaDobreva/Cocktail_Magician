using CocktailMagician.Data;
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
    public class GetBarsOfCocktailAsync_Should
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

            var ingredientName = "Name";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImageUrl = imageURLTest
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
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetBarsOfCocktailAsync(invalidId));
            };
        }
        [TestMethod]
        public async Task ThrowException_CocktailIsDeleted()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingredientName
                = "IngrToUpdate";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImageUrl = imageURLTest,
                IsDeleted = true
            };

            var options = TestUtilities.GetOptions(nameof(ThrowException_CocktailIsDeleted));

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
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.GetBarsOfCocktailAsync(cocktailId));
            };
        }
        [TestMethod]
        public async Task ReturnAllBarsOfCocktail()
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
                ImageUrl = CocktailImageUrlTest
            };

            var barImagaUrlTest = "https://www.google.com/";
            var bar1Name = "Name1Test";
            var bar2Name = "Name2est";
            var notIncludedBarName = "Name3est";


            var addressTest = new Address
            {
                Name = "AddressTest",
                City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var bar1 = new Bar
            {
                Name = bar1Name,
                ImageUrl = barImagaUrlTest,
                Address = addressTest,
            };
            var bar2 = new Bar
            {
                Name = bar2Name,
                ImageUrl = barImagaUrlTest,
                Address = addressTest,
            };
            var notIncudedBar = new Bar
            {
                Name = notIncludedBarName,
                ImageUrl = barImagaUrlTest,
                Address = addressTest,
            };

            var options = TestUtilities.GetOptions(nameof(ReturnAllBarsOfCocktail));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                {
                    Cocktail = cocktail,
                    Ingredient = ingredient,
                    Quatity = quantityTest
                });

                arrangeContext.BarCocktails.AddRange(new List<BarCocktail>
                {
                    new BarCocktail
                    {
                        Cocktail = cocktail,
                        Bar = bar1
                    },
                    new BarCocktail
                    {
                        Cocktail = cocktail,
                        Bar = bar2
                    }
                });

                arrangeContext.Bars.Add(notIncudedBar);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                var listOfBars = await sut.GetBarsOfCocktailAsync(cocktailId);

                Assert.AreEqual(2, listOfBars.Count());
                Assert.IsFalse(listOfBars.Any(b => b.Name == notIncludedBarName));
            }
        }

    }
}
