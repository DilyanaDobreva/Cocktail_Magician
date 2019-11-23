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
    public class SerchResultCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnCount_WhenFileterdByName()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var substringToSearch = "BC";

            var cocktail1NameTest = "abCd";
            var cocktail2NameTest = "ghjg";
            var cocktail3NameTest = "Bcgh";
            var cocktail4NameTest = "kjgh";

            var imageURLTest = "https://www.google.com/";

            var ingr1Name = "Ingr1";
            var ingr2Name = "Ingr2";
            var ingr3Name = "Ingr3";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient1 = new Ingredient
            {
                Name = ingr1Name,
                Unit = ingrUnitTest
            };
            var ingredient2 = new Ingredient
            {
                Name = ingr2Name,
                Unit = ingrUnitTest
            };
            var ingredient3 = new Ingredient
            {
                Name = ingr3Name,
                Unit = ingrUnitTest
            };

            var cocktail1 = new Cocktail
            {
                Name = cocktail1NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 3
                    }
                }
            };
            var cocktail2 = new Cocktail
            {
                Name = cocktail2NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }

            };
            var cocktail3 = new Cocktail
            {
                Name = cocktail3NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }
            };
            var cocktail4 = new Cocktail
            {
                Name = cocktail4NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 5
                    }
                }
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenFileterdByName));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
},
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail3,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    }
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                var dtoTest = new CocktailSearchDTO
                {
                    NameKey = substringToSearch,
                };
                var resultCount = await sut.SerchResultCountAsync(dtoTest);

                Assert.AreEqual(2, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenFilterByIngredient()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktail1NameTest = "abCd";
            var cocktail2NameTest = "ghjg";
            var cocktail3NameTest = "Bcgh";
            var cocktail4NameTest = "kjgh";

            var imageURLTest = "https://www.google.com/";

            var ingr1Name = "Ingr1";
            var ingr2Name = "Ingr2";
            var ingr3Name = "Ingr3";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient1 = new Ingredient
            {
                Name = ingr1Name,
                Unit = ingrUnitTest
            };
            var ingredient2 = new Ingredient
            {
                Name = ingr2Name,
                Unit = ingrUnitTest
            };
            var ingredient3 = new Ingredient
            {
                Name = ingr3Name,
                Unit = ingrUnitTest
            };

            var ingredientToSearch = ingredient2;

            var cocktail1 = new Cocktail
            {
                Name = cocktail1NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 3
                    }
                }
            };
            var cocktail2 = new Cocktail
            {
                Name = cocktail2NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }

            };
            var cocktail3 = new Cocktail
            {
                Name = cocktail3NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }
            };
            var cocktail4 = new Cocktail
            {
                Name = cocktail4NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 5
                    }
                }
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenFilterByIngredient));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
},
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail3,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    }
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var ingredientId = await assertContext.Ingredients.Where(i => i.Name == ingr2Name).Select(i => i.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);

                var DTOTest = new CocktailSearchDTO
                {
                    IngredientId = ingredientId
                };
                var resultCount = await sut.SerchResultCountAsync(DTOTest);

                Assert.AreEqual(2, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenFilterByRating()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var minRatingToFilter = 4;

            var cocktail1NameTest = "abCd";
            var cocktail2NameTest = "ghjg";
            var cocktail3NameTest = "Bcgh";
            var cocktail4NameTest = "kjgh";

            var imageURLTest = "https://www.google.com/";

            var ingr1Name = "Ingr1";
            var ingr2Name = "Ingr2";
            var ingr3Name = "Ingr3";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient1 = new Ingredient
            {
                Name = ingr1Name,
                Unit = ingrUnitTest
            };
            var ingredient2 = new Ingredient
            {
                Name = ingr2Name,
                Unit = ingrUnitTest
            };
            var ingredient3 = new Ingredient
            {
                Name = ingr3Name,
                Unit = ingrUnitTest
            };

            var cocktail1 = new Cocktail
            {
                Name = cocktail1NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 3
                    }
                }
            };
            var cocktail2 = new Cocktail
            {
                Name = cocktail2NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }

            };
            var cocktail3 = new Cocktail
            {
                Name = cocktail3NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }
            };
            var cocktail4 = new Cocktail
            {
                Name = cocktail4NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 5
                    }
                }
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenFilterByRating));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
},
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail3,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    }
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);
                var DTOTest = new CocktailSearchDTO
                {
                    MinRating = minRatingToFilter
                };
                var resultCount = await sut.SerchResultCountAsync(DTOTest);

                Assert.AreEqual(3, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenFilterByMultipleParameters()
        {
            var cocktailFactoryMock = new Mock<ICocktailFactory>();
            var cocktailIngredinetFactoryMock = new Mock<ICocktailIngredientFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();

            var cocktail1NameTest = "abCd";
            var cocktail2NameTest = "ghjg";
            var cocktail3NameTest = "Bcgh";
            var cocktail4NameTest = "kjgh";

            var imageURLTest = "https://www.google.com/";

            var ingr1Name = "Ingr1";
            var ingr2Name = "Ingr2";
            var ingr3Name = "Ingr3";

            var ingrUnitTest = "Unit";
            var quantityTest = 0.5;

            var ingredient1 = new Ingredient
            {
                Name = ingr1Name,
                Unit = ingrUnitTest
            };
            var ingredient2 = new Ingredient
            {
                Name = ingr2Name,
                Unit = ingrUnitTest
            };
            var ingredient3 = new Ingredient
            {
                Name = ingr3Name,
                Unit = ingrUnitTest
            };

            var ingredientToSearch = ingredient2;
            var substringToSearh = "BC";
            var minRatingToSearch = 4;

            var cocktail1 = new Cocktail
            {
                Name = cocktail1NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 3
                    }
                }
            };
            var cocktail2 = new Cocktail
            {
                Name = cocktail2NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }
            };
            var cocktail3 = new Cocktail
            {
                Name = cocktail3NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 4
                    }
                }
            };
            var cocktail4 = new Cocktail
            {
                Name = cocktail4NameTest,
                ImageUrl = imageURLTest,
                CocktailReviews = new List<CocktailReview>
                {
                    new CocktailReview
                    {
                        Rating = 5
                    }
                }
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenFilterByMultipleParameters));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.CocktailIngredients.AddRange(new List<CocktailIngredient>
                    {
                    new CocktailIngredient
                    {
                        Cocktail = cocktail1,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
},
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient1,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail2,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail3,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient2,
                        Quatity = quantityTest
                    },
                    new CocktailIngredient
                    {
                        Cocktail = cocktail4,
                        Ingredient = ingredient3,
                        Quatity = quantityTest
                    }
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CocktailMagicianDb(options))
            {
                var ingredientId = await assertContext.Ingredients.Where(i => i.Name == ingr2Name).Select(i => i.Id).FirstAsync();
                var sut = new CocktailServices(assertContext, cocktailFactoryMock.Object, cocktailIngredinetFactoryMock.Object, barCocktailFactoryMock.Object);

                var DTOTest = new CocktailSearchDTO
                {
                    NameKey = substringToSearh,
                    IngredientId = ingredientId,
                    MinRating = minRatingToSearch
                };
                var resultCount = await sut.SerchResultCountAsync(DTOTest);

                Assert.AreEqual(1, resultCount);
            }
        }
    }
}
