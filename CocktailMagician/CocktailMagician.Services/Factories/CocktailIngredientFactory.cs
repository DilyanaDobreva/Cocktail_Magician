using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class CocktailIngredientFactory : ICocktailIngredientFactory
    {
        public CocktailIngredient Create(int cocktailId, int ingredientId, double quantity)
        {
            var ci = new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredientId,
                Quatity = quantity
            };

            return ci;
        }
    }
}
