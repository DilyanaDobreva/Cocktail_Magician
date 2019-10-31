﻿using System;
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
            if (ingredientsAndQuantities.Count() == 0)
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
            var cocktail = await context.Cocktails.Include(c => c.CocktailIngredients).FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            cocktail.CocktailIngredients.Select(i => i.IsDeleted = true);
            cocktail.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public async Task<Cocktail> Get(int id)
        {
            var cocktail = await context.Cocktails.Include(c => c.CocktailIngredients).FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return cocktail;
        }
        public async Task<List<CocktailInListDTO>> GetAllDTO()
        {
            var list = await context.Cocktails
                .Where(c => c.IsDeleted == false)
                .Select(c => new CocktailInListDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                   ImageURL = c.ImageUrl
                })
                .ToListAsync();
            return list;
        }
        public async Task AddIngredient(int cocktailId, int ingredientId, int quantity)
        {
            var existing = await context.CocktailIngredients.FirstOrDefaultAsync(ci => ci.IngredientId == ingredientId && ci.CocktailId == cocktailId);
            if (existing != null)
            {
                if (existing.IsDeleted == true)
                {
                    existing.IsDeleted = false;
                }
                existing.Quatity = quantity;
            }
            else
            {
                var newIngredient = cocktailIngredientFactory.Create(cocktailId, ingredientId, quantity);
                context.CocktailIngredients.Add(newIngredient);
            }

            await context.SaveChangesAsync();
        }
        public async Task RemoveIngredient(int cocktailId, int ingredientId)
        {
            var ingredient = await context.CocktailIngredients
                .FirstOrDefaultAsync(ci => ci.IngredientId == ingredientId && ci.CocktailId == cocktailId && ci.IsDeleted==false);

            if(ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }

            ingredient.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }
}
