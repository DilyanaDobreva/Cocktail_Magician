﻿using System;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
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
            var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            if (ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }
            ingredient.IsDeleted = true;
        }

        public async Task<Ingredient> Get(int id)
        {
            var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            return ingredient;
        }
    }
}
