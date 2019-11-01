using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface ICocktailFactory
    {
        Cocktail Create(string name, string imageURL);
    }
}