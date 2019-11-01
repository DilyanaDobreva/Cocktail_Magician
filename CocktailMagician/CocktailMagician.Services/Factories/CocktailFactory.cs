using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class CocktailFactory : ICocktailFactory
    {
        public Cocktail Create(string name, string imageURL)
        {
            var cocktail = new Cocktail
            {
                Name = name,
                ImageUrl = imageURL
            };
            return cocktail;
        }
    }
}
