using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class CocktailFactory : ICocktailFactory
    {
        public Cocktail Create(string name)
        {
            var cocktail = new Cocktail
            {
                Name = name
            };
            return cocktail;
        }
    }
}
