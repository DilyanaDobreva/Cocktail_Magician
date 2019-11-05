using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class BarCocktailFactory : IBarCocktailFactory
    {
        public BarCocktail Create(int barId, int cocktailId)
        {
            var barCocktail = new BarCocktail
            {
                BarId = barId,
                CocktailId = cocktailId
            };
            return barCocktail;
        }
    }
}
