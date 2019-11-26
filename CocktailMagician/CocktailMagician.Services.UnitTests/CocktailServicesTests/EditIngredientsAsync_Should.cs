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
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.CocktailServicesTests
{
    [TestClass]
    public class EditIngredientsAsync_Should
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

            var ingrToUpdateNameTest = "IngrToUpdate";
            var ingrToRemoveNameTest = "IngrToRemove";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredientToUpdate = new Ingredient
            {
                Name = ingrToUpdateNameTest,
                Unit = ingrUnitTest
            };

            var ingredientToRemove = new Ingredient
            {
                Name = ingrToRemoveNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToUpdateNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveNameTest };

            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToUpdate,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToRemove,
                        Quatity = quantityTest
                    }
                });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditIngredientsAsync(invalidId, newIistIngrQuantity, listIngrToRemove));
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

            var ingrToUpdateNameTest = "IngrToUpdate";
            var ingrToRemoveNameTest = "IngrToRemove";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredientToUpdate = new Ingredient
            {
                Name = ingrToUpdateNameTest,
                Unit = ingrUnitTest
            };

            var ingredientToRemove = new Ingredient
            {
                Name = ingrToRemoveNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
                IsDeleted = true
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToUpdateNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveNameTest };

            var options = TestUtilities.GetOptions(nameof(ThrowException_CocktailIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToUpdate,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToRemove,
                        Quatity = quantityTest
                    }
                });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditIngredientsAsync(cocktailId, newIistIngrQuantity, listIngrToRemove));
            };
        }
        [TestMethod]
        public async Task ThrowsException_WhenIngredientToAddIsInvalid()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrToUpdateIvalidNameTest = "IngrToUpdate";
            var ingrToRemoveNameTest = "IngrToRemove";
            var ingrNameTest = "Ingredient";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var ingredientToRemove = new Ingredient
            {
                Name = ingrToRemoveNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToUpdateIvalidNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveNameTest };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIngredientToAddIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredient,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToRemove,
                        Quatity = quantityTest
                    }
                });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditIngredientsAsync(cocktailId, newIistIngrQuantity, listIngrToRemove));
            };

        }
        [TestMethod]
        public async Task ThworsException_WhenIngredientToAddIsDeleted()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrToAddNameTest = "IngrToAdd";
            var ingrToRemoveNameTest = "IngrToRemove";
            var ingrNameTest = "Ingredient";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredient = new Ingredient
            {
                Name = ingrNameTest,
                Unit = ingrUnitTest
            };

            var ingredientToAdd = new Ingredient
            {
                Name = ingrToAddNameTest,
                Unit = ingrUnitTest,
                IsDeleted = true
            };

            var ingredientToRemove = new Ingredient
            {
                Name = ingrToRemoveNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToAddNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveNameTest };

            var options = TestUtilities.GetOptions(nameof(ThworsException_WhenIngredientToAddIsDeleted));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredient,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToRemove,
                        Quatity = quantityTest
                    }
                });

                arrangeContext.Ingredients.Add(ingredientToAdd);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditIngredientsAsync(cocktailId, newIistIngrQuantity, listIngrToRemove));
            };
        }
        [TestMethod]
        public async Task ThrowsException_WhenIngredindToRemoveIsInvalid()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrToUpdateNameTest = "IngrToUpdate";
            var ingrToRemoveInvalidNameTest = "IngrToRemove";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredientToUpdate = new Ingredient
            {
                Name = ingrToUpdateNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToUpdateNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveInvalidNameTest };

            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenIngredindToRemoveIsInvalid));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.Add(new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToUpdate,
                        Quatity = quantityTest
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await assertContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.EditIngredientsAsync(cocktailId, newIistIngrQuantity, listIngrToRemove));
            };
        }
        [TestMethod]
        public async Task EditsCocktailIngredients()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktailNameTest = "TestName";
            var imageURLTest = "https://www.google.com/";

            var ingrToUpdateNameTest = "IngrToUpdate";
            var ingrToRemoveNameTest = "IngrToRemove";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var quantityToUpdate = 1;

            var ingredientToUpdate = new Ingredient
            {
                Name = ingrToUpdateNameTest,
                Unit = ingrUnitTest
            };

            var ingredientToRemove = new Ingredient
            {
                Name = ingrToRemoveNameTest,
                Unit = ingrUnitTest
            };

            var cocktail = new Cocktail
            {
                Name = cocktailNameTest,
                ImagePath = imageURLTest,
            };

            var newIistIngrQuantity = new List<CocktailIngredientDTO>
                { new CocktailIngredientDTO
                    {
                        Name = ingrToUpdateNameTest,
                        Value = quantityToUpdate,
                        Unit = ingrUnitTest
                    }
                };

            var listIngrToRemove = new List<string> { ingrToRemoveNameTest };

            var options = TestUtilities.GetOptions(nameof(EditsCocktailIngredients));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToUpdate,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail,
                        Ingredient = ingredientToRemove,
                        Quatity = quantityTest
                    }
                });

                await arrangeContext.SaveChangesAsync();
            }
            using (var actContext = new CocktailMagicianDb(options))
            {
                var cocktailId = await actContext.Cocktails.Where(c => c.Name == cocktailNameTest).Select(c => c.Id).FirstAsync();

                var sut = new CocktailServices(actContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                await sut.EditIngredientsAsync(cocktailId, newIistIngrQuantity, listIngrToRemove);
            };

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cocktailInDB = await assertContext.Cocktails
                    .Include(c => c.CocktailIngredients)
                    .ThenInclude(ci => ci.Ingredient)
                    .FirstAsync(c => c.Name == cocktailNameTest);

                Assert.AreEqual(1, cocktailInDB.CocktailIngredients.Count());
                Assert.IsTrue(cocktailInDB.CocktailIngredients.Any(i => i.Ingredient.Name == ingrToUpdateNameTest));
            }
        }
    }
}
