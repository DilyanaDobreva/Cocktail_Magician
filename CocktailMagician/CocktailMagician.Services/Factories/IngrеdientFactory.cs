using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class IngredientFactory : IIngredientFactory
    {
        public Ingredient Create(string name)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                IsDeleted = false
            };

            return ingredient;
        }
    }
}
