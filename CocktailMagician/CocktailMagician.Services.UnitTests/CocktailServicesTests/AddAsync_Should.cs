using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
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
    public class AddAsync_Should
    {
        [TestMethod]
        public async Task ThrowsException_WhenNoCocktailIngredientsInList()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var listOfIngr = new List<CocktailIngredientDTO>();

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenNoCocktailIngredientsInList));

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddAsync(cocktailNameTest, imageURLTest, listOfIngr));
            }
        }
        [TestMethod]
        public async Task ThrowsException_WhenListOfCocktailIngredientsIsNull()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

           
            List<CocktailIngredientDTO> listOfIngr = null;

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenListOfCocktailIngredientsIsNull));

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddAsync(cocktailNameTest, imageURLTest, listOfIngr));
            }
        }
        [TestMethod]
        public async Task ThrowException_WhenCocktailIngredientHasNoQuantity()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.0;

            var cocktailIngredientDTO = new CocktailIngredientDTO
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest,
                Value = quantityTest
            };

            var listOfIngr = new List<CocktailIngredientDTO> { cocktailIngredientDTO };

            var cocktailTest = new Cocktail
            {
                Name = cocktailNameTest,
                ImageUrl = imageURLTest
            };

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenCocktailIngredientHasNoQuantity));

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.AddAsync(cocktailNameTest, imageURLTest, listOfIngr));
            }
        }
        [TestMethod]
        public async Task AddCocktailToDB()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrNameTest = "IngrTest";
            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var cocktailIngredientDTO = new CocktailIngredientDTO
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest,
                Value = quantityTest
            };

            var listOfIngr = new List<CocktailIngredientDTO> { cocktailIngredientDTO };

            var cocktailTest = new Cocktail
            {
                Name = cocktailNameTest,
                ImageUrl = imageURLTest
            };

            cocktailFactoryMock
                .Setup(f => f.Create(cocktailNameTest, imageURLTest))
                .Returns(cocktailTest);

            var options = TestUtilities.GetOptions(nameof(AddCocktailToDB));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Ingredients.Add(new Ingredient { Name = ingrNameTest, Unit = ingrUnitTest });
                await arrangeContext.SaveChangesAsync();
            }

            using (var actContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(actContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);

                var ingrId = await actContext.Ingredients.Where(i => i.Name == ingrNameTest).Select(i => i.Id).FirstAsync();
                cocktailIngredinetFactoryMock
                    .Setup(f => f.Create(cocktailTest, ingrId, quantityTest))
                    .Returns(new CocktailIngredient
                    {
                        Cocktail = cocktailTest,
                        IngredientId = ingrId,
                        Quatity = quantityTest
                    });

                await sut.AddAsync(cocktailNameTest, imageURLTest, listOfIngr);
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktail = await assertContext.Cocktails.Include(c => c.CocktailIngredients).FirstAsync();

                Assert.AreEqual(1, assertContext.Cocktails.Count());
                Assert.AreEqual(cocktailNameTest, cocktail.Name);
                Assert.AreEqual(1, cocktail.CocktailIngredients.Count());
            }
        }
    }
}
