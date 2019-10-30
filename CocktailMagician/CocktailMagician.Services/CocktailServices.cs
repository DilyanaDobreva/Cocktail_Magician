using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CocktailMagicianDb context;
        private readonly ICocktailFactory cocktailFactory;
        private readonly ICocktailIngredientFactory cocktailIngredientFactory;

        public CocktailServices(CocktailMagicianDb context, ICocktailFactory cocktailFactory, ICocktailIngredientFactory cocktailIngredientFactory)
        {
            this.context = context;
            this.cocktailFactory = cocktailFactory;
            this.cocktailIngredientFactory = cocktailIngredientFactory;
        }

        public async Task Add(string name, Dictionary<int, int> ingredientsAndQuantities)
        {
            if(ingredientsAndQuantities.Count() == 0)
            {
                throw new ArgumentException(OutputConstants.CocktailWithNoIngredients);
            }
            var cocktail = this.cocktailFactory.Create(name);

            context.Cocktails.Add(cocktail);
            await context.SaveChangesAsync();

            //TODO How to get only Cocktail Id from DB
            cocktail = await context.Cocktails.FirstAsync(c => c.Name == name);
            
            foreach (var ingredient in ingredientsAndQuantities)
            {
                var cocktailIngredient = this.cocktailIngredientFactory.Create(cocktail.Id, ingredient.Key, ingredient.Value);
                context.CocktailIngredients.Add(cocktailIngredient);
            }

            await context.SaveChangesAsync();


        }

        public async Task Delete(int id)
        {
            var cocktail = await context.Cocktails.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
