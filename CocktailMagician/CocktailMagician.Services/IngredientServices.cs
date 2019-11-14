using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
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

        public async Task<IngredientBasicDTO> AddAsync(string name, string unit)
        {
            var ingredient = ingredientFactory.Create(name, unit);
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            var dbIngredient = await context.Ingredients
                .Select(i => new IngredientBasicDTO
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Unit = ingredient.Unit

                })
                .SingleAsync(i => i.Name == name);

            return dbIngredient;
        }
        //public async Task EditAsync(int id, string newName)
        //{
        //    var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        //    if (ingredient == null)
        //    {
        //        throw new ArgumentException(OutputConstants.IngredientNotFound);
        //    }

        //    ingredient.Name = newName;
        //    await context.SaveChangesAsync();
        //}
        public async Task DeleteAsync(int id)
        {
            if (!await context.Ingredients.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            if(await context.CocktailIngredients.AnyAsync(ci => ci.IngredientId == id))
            {
                throw new InvalidOperationException(OutputConstants.IngredientPartOfCocktails);
            }

            var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            
            ingredient.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        //public async Task<Ingredient> GetAsync(int id)
        //{
        //    var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        //    return ingredient;
        //}
        public async Task<List<IngredientBasicDTO>> GetAllDTOAsync()
        {
            var allIngredients = await context.Ingredients
                .Include(i => i.CocktailIngredients)
                .Where(i => i.IsDeleted == false)
                .Select(i => new IngredientBasicDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Unit = i.Unit,
                    CanDelete = !i.CocktailIngredients.Any()
                })
                .OrderBy(i => i.Name)
                .ToListAsync();

            return allIngredients;
        }
        public async Task<List<IngredientBasicDTO>> GetAllPagedDTOAsync(int itemsPerPage, int currentPage)
        {
            var allIngredients = await context.Ingredients
                .Include(i => i.CocktailIngredients)
                .Where(i => i.IsDeleted == false)
                .Select(i => new IngredientBasicDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Unit = i.Unit,
                    CanDelete = !i.CocktailIngredients.Any()
                })
                .OrderBy(i => i.Name)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return allIngredients;
        }
        public async Task<int> AllIngredientsCountAsync()
        {
            var count = await context.Ingredients.Where(c => c.IsDeleted == false).CountAsync();
            return count;
        }

    }
}
