using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class IngredientServices : IIngredientServices
    {
        private readonly CocktailMagicianDb context;
        private readonly IIngredientFactory ingredientFactory;

        public IngredientServices(CocktailMagicianDb context, IIngredientFactory ingredientFactory)
        {
            this.context = context;
            this.ingredientFactory = ingredientFactory;
        }

        public async Task Add(string name)
        {
            var ingredient = ingredientFactory.Create(name);
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
        }

        public async Task Edit(int id, string newName)
        {
            var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            if(ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }

            ingredient.Name = newName;
            await context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var ingredient = await context.Ingredients.Include(i => i.CocktailIngredients).FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            if (ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }
            //TODO Why cannot use AnyAsync
            if(ingredient.CocktailIngredients.Any(ci => ci.IsDeleted == false))
            {
                throw new ArgumentException(OutputConstants.CoctailIncludeIngredient);
            }
            ingredient.IsDeleted = true;
        }

        public async Task<Ingredient> Get(int id)
        {
            var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            return ingredient;
        }

        public async Task<List<IngredientDTO>> GetAllDTO()
        {
            var ingredients = await context.Ingredients
                .Where(i => i.IsDeleted == false)
                .Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();

            return ingredients;
        }
    }
}
