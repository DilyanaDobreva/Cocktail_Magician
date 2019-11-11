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

            var dbIngredient = await context.Ingredients.SingleAsync(i => i.Name == name);
            return dbIngredient.MapToDTO();
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
            var ingredient = await context.Ingredients.Include(i => i.CocktailIngredients).FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            if (ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }
            //TODO Why cannot use AnyAsync
            
            ingredient.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        //public async Task<Ingredient> GetAsync(int id)
        //{
        //    var ingredient = await context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        //    return ingredient;
        //}

        public async Task<List<IngredientBasicDTO>> GetAllNotIncludedDTOAsync(int cocktailId)
        {
            var ingredients = await context.Ingredients
                .Include(i => i.CocktailIngredients)
                .Where(i => i.IsDeleted == false && !(i.CocktailIngredients.Any(ingr => ingr.CocktailId == cocktailId)))
                .Select(i => new IngredientBasicDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Unit = i.Unit
                })
                .ToListAsync();

            return ingredients;
        }
        public async Task<List<IngredientBasicDTO>> GetAllDTOAsync()
        {
            var allIngredients = await context.Ingredients
                .Where(i => i.IsDeleted == false)
                .Select(i => new IngredientBasicDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Unit = i.Unit,
                })
                .OrderBy(i => i.Name)
                .ToListAsync();

            allIngredients.ForEach(i => i.CanDelete = CanDelete(i.Id));
            return allIngredients;
        }
        public async Task<List<IngredientBasicDTO>> GetAllPagedDTOAsync(int itemsPerPage, int currentPage)
        {
            var allIngredients = await context.Ingredients
                .Where(i => i.IsDeleted == false)
                .Select(i => new IngredientBasicDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Unit = i.Unit,
                })
                .OrderBy(i => i.Name)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            allIngredients.ForEach(i => i.CanDelete = CanDelete(i.Id));
            return allIngredients;
        }


        private bool CanDelete(int id)
        {
            var canDelete = !context.CocktailIngredients.Any(b => b.IngredientId == id);
            return canDelete;
        }
        public async Task<int> AllIngredientsCountAsync()
        {
            var count = await context.Ingredients.Where(c => c.IsDeleted == false).CountAsync();
            return count;
        }

    }
}
