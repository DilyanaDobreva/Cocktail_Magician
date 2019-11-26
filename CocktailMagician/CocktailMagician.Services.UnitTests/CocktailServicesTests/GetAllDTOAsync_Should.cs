using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
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
    public class GetAllDTOAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllNotDeletedCocktails()
        {
            {
                var cocktailFactoryMock = new Mock<ICocktailFactory>();
                var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
                var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

                var cocktail1NameTest = "TestName1";
                var cocktail2NameTest = "TestName2";

                var CocktailImageUrlTest = "https://www.google.com/";

                var ingrNameTest = "IngrTest";
                var ingrUnitTest = "Unit";
                var quantityTest = 0.5;

                var ingredient = new Ingredient
                {
                    Name = ingrNameTest,
                    Unit = ingrUnitTest
                };

                var cocktail1 = new Cocktail
                {
                    Name = cocktail1NameTest,
                    ImagePath = CocktailImageUrlTest
                };

                var cocktail2 = new Cocktail
                {
                    Name = cocktail2NameTest,
                    ImagePath = CocktailImageUrlTest,
                    IsDeleted = true
                };

                var options = TestUtilities.GetOptions(nameof(ReturnAllNotDeletedCocktails));

                using (var arrangeContext = new CocktailMagicianDb(options))
                {
                    arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient,
                        Quatity = quantityTest
                    });

                    arrangeContext.Cocktails.Add(cocktail2);
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new CocktailMagicianDb(options))
                {
                    var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                    var list = await sut.GetAllDTOAsync(5,1);

                    Assert.AreEqual(1, list.Count());
                    Assert.IsTrue(list.Any(c => c.Name == cocktail1NameTest));
                }
            }
        }
    }
}
