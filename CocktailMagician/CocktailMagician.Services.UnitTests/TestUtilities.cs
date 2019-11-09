using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMagician.Services.UnitTests
{
    public class TestUtilities
    {
        public static DbContextOptions<CocktailMagicianDb> GetOptions(string databaseName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<CocktailMagicianDb>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }
    }
}

