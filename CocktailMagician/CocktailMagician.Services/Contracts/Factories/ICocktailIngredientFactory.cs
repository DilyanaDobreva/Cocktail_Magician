using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface ICocktailIngredientFactory
    {
        CocktailIngredient Create(Cocktail cocktail, int ingredientId, double quantity);
        CocktailIngredient Create(int cocktailId, int ingredientId, double quantity);

    }
}