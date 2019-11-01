using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface ICityFactory
    {
        City Create(string name);
    }
}
