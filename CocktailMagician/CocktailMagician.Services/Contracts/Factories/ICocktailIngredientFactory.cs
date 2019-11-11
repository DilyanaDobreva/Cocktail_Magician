using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface ICocktailIngredientFactory
    {
        CocktailIngredient Create(int cocktailId, int ingredientId, double quantity);
    }
}