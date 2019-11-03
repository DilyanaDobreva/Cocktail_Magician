using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class IngredientFactory : IIngredientFactory
    {
        public Ingredient Create(string name, string unit)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Unit = unit,
                IsDeleted = false
            };

            return ingredient;
        }
    }
}
