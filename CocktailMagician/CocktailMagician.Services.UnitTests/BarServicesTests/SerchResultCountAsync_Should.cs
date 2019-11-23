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

namespace CocktailMagician.Services.UnitTests.BarServicesTests
{
    [TestClass]
    public class SerchResultCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnCount_WhenSearchByName()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();
            var imagaUrlTest = "https://www.google.com/";
            var bar1TestName = "abC";
            var bar2TestName = "Def";
            var bar3TestName = "Bci";
            var bar4TestName = "jkl";


            var addressTest = new Address
            {
                Name = "AddressTest",
                //City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenSearchByName));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.AddRange(new List<City>
                {
                    new City { Name = "Sofia" },
                    new City { Name = "Varna" }
                });
                await arrangeContext.SaveChangesAsync();

                var city1Id = await arrangeContext.Cities.Where(c => c.Name == "Sofia").Select(c => c.Id).FirstAsync();
                var city2Id = await arrangeContext.Cities.Where(c => c.Name == "Varna").Select(c => c.Id).FirstAsync();

                var bar1 = new Bar
                {
                    Name = bar1TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 3 }
                    }
                };
                bar1.Address.CityId = city1Id;

                var bar2 = new Bar
                {
                    Name = bar2TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };
                bar2.Address.CityId = city1Id;

                var bar3 = new Bar
                {
                    Name = bar3TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };
                bar3.Address.CityId = city2Id;

                var bar4 = new Bar
                {
                    Name = bar4TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 5 }
                    }
                };
                bar3.Address.CityId = city2Id;

                arrangeContext.Bars.AddRange(new List<Bar> { bar1, bar2, bar3, bar4 });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var substringToSearch = "Bc";

                var dtoTest = new BarSearchDTO
                {
                    CityId = null,
                    MinRating = null,
                    NameKey = substringToSearch
                };

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var resultCount = await sut.SerchResultCountAsync(dtoTest);

                Assert.AreEqual(2, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenSearchByRating()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();
            var imagaUrlTest = "https://www.google.com/";
            var bar1TestName = "abC";
            var bar2TestName = "Def";
            var bar3TestName = "Bci";
            var bar4TestName = "jkl";


            var addressTest = new Address
            {
                Name = "AddressTest",
                //City = new City { Name = "SofiaTest" },
                Latitude = 1.1,
                Longitude = 1.1
            };

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenSearchByRating));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.AddRange(new List<City>
                {
                    new City { Name = "Sofia" },
                    new City { Name = "Varna" }
                });
                await arrangeContext.SaveChangesAsync();

                var city1Id = await arrangeContext.Cities.Where(c => c.Name == "Sofia").Select(c => c.Id).FirstAsync();
                var city2Id = await arrangeContext.Cities.Where(c => c.Name == "Varna").Select(c => c.Id).FirstAsync();

                var bar1 = new Bar
                {
                    Name = bar1TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 3 }
                    }
                };
                bar1.Address.CityId = city1Id;

                var bar2 = new Bar
                {
                    Name = bar2TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };
                bar2.Address.CityId = city1Id;

                var bar3 = new Bar
                {
                    Name = bar3TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };
                bar3.Address.CityId = city2Id;

                var bar4 = new Bar
                {
                    Name = bar4TestName,
                    ImageUrl = imagaUrlTest,
                    Address = addressTest,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 5 }
                    }
                };
                bar3.Address.CityId = city2Id;

                arrangeContext.Bars.AddRange(new List<Bar> { bar1, bar2, bar3, bar4 });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var minRatingToSearch = 4;

                var dtoTest = new BarSearchDTO
                {
                    CityId = null,
                    MinRating = minRatingToSearch,
                    NameKey = null
                };

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var resultCount = await sut.SerchResultCountAsync(dtoTest);

                Assert.AreEqual(3, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenSearchByCityId()
        {
            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();
            var imagaUrlTest = "https://www.google.com/";

            var bar1TestName = "abC";
            var bar2TestName = "Def";
            var bar3TestName = "Bci";
            var bar4TestName = "jkl";

            var city1Name = "Sofia";
            var city2Name = "Varna";

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenSearchByCityId));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.AddRange(new List<City>
                {
                    new City { Name = city1Name },
                    new City { Name = city2Name }
                });
                await arrangeContext.SaveChangesAsync();

                var city1Id = await arrangeContext.Cities.Where(c => c.Name == city1Name).Select(c => c.Id).FirstAsync();
                var city2Id = await arrangeContext.Cities.Where(c => c.Name == city2Name).Select(c => c.Id).FirstAsync();

                var address1Test = new Address
                {
                    Name = "Address1Test",
                    CityId = city1Id,
                    Latitude = 1.1,
                    Longitude = 1.1
                };

                var address2Test = new Address
                {
                    Name = "Address2Test",
                    CityId = city2Id,
                    Latitude = 1.1,
                    Longitude = 1.1
                };

                var bar1 = new Bar
                {
                    Name = bar1TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 3 }
                    }
                };

                var bar2 = new Bar
                {
                    Name = bar2TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address2Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };

                var bar3 = new Bar
                {
                    Name = bar3TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };

                var bar4 = new Bar
                {
                    Name = bar4TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 5 }
                    }
                };

                arrangeContext.Bars.AddRange(new List<Bar> { bar1, bar2, bar3, bar4 });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var cityIdToSearch = await assertContext.Cities.Where(c => c.Name == city1Name).Select(c => c.Id).FirstAsync();

                var dtoTest = new BarSearchDTO
                {
                    CityId = cityIdToSearch,
                    MinRating = null,
                    NameKey = null
                };

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var resultCount = await sut.SerchResultCountAsync(dtoTest);

                Assert.AreEqual(3, resultCount);
            }
        }
        [TestMethod]
        public async Task ReturnCount_WhenSearchByMultipleParameters()
        {

            var barFactoryMock = new Mock<IBarFactory>();
            var barCocktailFactoryMock = new Mock<IBarCocktailFactory>();
            var imagaUrlTest = "https://www.google.com/";

            var bar1TestName = "abC";
            var bar2TestName = "Def";
            var bar3TestName = "Bci";
            var bar4TestName = "jkl";

            var city1Name = "Sofia";
            var city2Name = "Varna";

            var options = TestUtilities.GetOptions(nameof(ReturnCount_WhenSearchByMultipleParameters));

            using (var arrangeContext = new CocktailMagicianDb(options))
            {
                arrangeContext.Cities.AddRange(new List<City>
                {
                    new City { Name = city1Name },
                    new City { Name = city2Name }
                });
                await arrangeContext.SaveChangesAsync();

                var city1Id = await arrangeContext.Cities.Where(c => c.Name == city1Name).Select(c => c.Id).FirstAsync();
                var city2Id = await arrangeContext.Cities.Where(c => c.Name == city2Name).Select(c => c.Id).FirstAsync();

                var address1Test = new Address
                {
                    Name = "Address1Test",
                    CityId = city1Id,
                    Latitude = 1.1,
                    Longitude = 1.1
                };

                var address2Test = new Address
                {
                    Name = "Address2Test",
                    CityId = city2Id,
                    Latitude = 1.1,
                    Longitude = 1.1
                };

                var bar1 = new Bar
                {
                    Name = bar1TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 3 }
                    }
                };

                var bar2 = new Bar
                {
                    Name = bar2TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address2Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };

                var bar3 = new Bar
                {
                    Name = bar3TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 4 }
                    }
                };

                var bar4 = new Bar
                {
                    Name = bar4TestName,
                    ImageUrl = imagaUrlTest,
                    Address = address1Test,
                    BarReviews = new List<BarReview>
                    {
                        new BarReview { Rating = 5 }
                    }
                };

                arrangeContext.Bars.AddRange(new List<Bar> { bar1, bar2, bar3, bar4 });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianDb(options))
            {
                var substringToSearch = "Bc";
                var cityIdToSearch = await assertContext.Cities.Where(c => c.Name == city1Name).Select(c => c.Id).FirstAsync();
                var minRatingToSearch = 4;

                var dtoTest = new BarSearchDTO
                {
                    CityId = cityIdToSearch,
                    MinRating = minRatingToSearch,
                    NameKey = substringToSearch
                };

                var sut = new BarServices(assertContext, barFactoryMock.Object, barCocktailFactoryMock.Object);
                var resultCount = await sut.SerchResultCountAsync(dtoTest);

                Assert.AreEqual(1, resultCount);
            }
        }

    }
}
