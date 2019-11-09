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
    public class CocktailServices : ICocktailServices
    {
        private readonly CocktailMagicianDb context;
        private readonly ICocktailFactory cocktailFactory;
        private readonly ICocktailIngredientFactory cocktailIngredientFactory;
        private readonly ICocktailReviewServices cocktailReviewServices;
        private readonly IBarCocktailFactory barCocktailFactory;

        public CocktailServices(CocktailMagicianDb context, ICocktailFactory cocktailFactory, ICocktailIngredientFactory cocktailIngredientFactory, ICocktailReviewServices cocktailReviewServices,
            IBarCocktailFactory barCocktailFactory)
        {
            this.context = context;
            this.cocktailFactory = cocktailFactory;
            this.cocktailIngredientFactory = cocktailIngredientFactory;
            this.cocktailReviewServices = cocktailReviewServices;
            this.barCocktailFactory = barCocktailFactory;
        }

        public async Task Add(string name, string imageURL, List<CocktailIngredientDTO> ingredientsAndQuantities)
        {
            if (ingredientsAndQuantities.Count() == 0)
            {
                throw new ArgumentException(OutputConstants.CocktailWithNoIngredients);
            }
            var cocktail = this.cocktailFactory.Create(name, imageURL);
            cocktail.Quantity = ingredientsAndQuantities.Sum(i => i.Value);
            context.Cocktails.Add(cocktail);
            await context.SaveChangesAsync();

            //TODO How to get only Cocktail Id from DB
            cocktail = await context.Cocktails.FirstAsync(c => c.Name == name);

            foreach (var ingredient in ingredientsAndQuantities)
            {
                var ingrId = await context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredient.Name && i.IsDeleted == false);
                var cocktailIngredient = this.cocktailIngredientFactory.Create(cocktail.Id, ingrId.Id, ingredient.Value);
                context.CocktailIngredients.Add(cocktailIngredient);
            }
            await context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var cocktail = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                .Include(c => c.BarCocktails)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            cocktail.CocktailIngredients.Select(i => i.IsDeleted = true);
            cocktail.BarCocktails.Select(bc => bc.IsDeleted = true);
            cocktail.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public async Task<CocktailDetailsDTO> GetDTO(int id)
        {
            var cocktail = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                    .ThenInclude(i => i.Ingredient)
                .Include(c => c.BarCocktails)
                    .ThenInclude(b => b.Bar)
                        .ThenInclude(b => b.Address)
                            .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            var cocktailDTO = cocktail.MapToDetailsDTO();
            return cocktailDTO;
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
            //To Check K.
            foreach (var item in list)
            {
                item.AverageRating = await cocktailReviewServices.GetMidRatingAsync(item.Id);
            }
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
                .FirstOrDefaultAsync(ci => ci.IngredientId == ingredientId && ci.CocktailId == cocktailId && ci.IsDeleted == false);

            if (ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }

            ingredient.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public Task<string> GetName(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }
            var cokctailName = context.Cocktails.Where(c => c.Id == id).Select(c => c.Name).FirstAsync();

            return cokctailName;
        }
        public async Task AddBarsAsync(int cocktailID, List<int> barsId)
        {
            foreach (var id in barsId)
            {
                var barCocktail = await context.BarCocktails.FirstOrDefaultAsync(bc => bc.CocktailId == cocktailID && bc.BarId == id);
                if (barCocktail != null)
                {
                    barCocktail.IsDeleted = false;
                }
                else
                {
                    barCocktail = barCocktailFactory.Create(id, cocktailID);
                    context.BarCocktails.Add(barCocktail);
                }
            }

            await context.SaveChangesAsync();
        }
        public async Task RemoveBarsAsync(int cocktailID, List<int> barsId)
        {
            foreach (var id in barsId)
            {
                var barCocktail = await context.BarCocktails.FirstOrDefaultAsync(bc => bc.CocktailId == cocktailID && bc.BarId == id);
                if (barCocktail != null)
                {
                    barCocktail.IsDeleted = true;
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task EditIngredients(int cocktailId, List<CocktailIngredientDTO> newIngredients, List<string> ingrToRemove)
        {
            if (cocktailId == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var currentIngredients = await context.CocktailIngredients
                .Include(i => i.Ingredient)
                .Where(c => c.CocktailId == cocktailId)
                .ToListAsync();

            foreach (var ingr in newIngredients)
            {
                var cocktailIngredient = currentIngredients.FirstOrDefault(i => i.Ingredient.Name == ingr.Name);

                if (cocktailIngredient != null)
                {
                    cocktailIngredient.Quatity = ingr.Value;
                    cocktailIngredient.IsDeleted = false;
                }
                else
                {
                    var singleIngredientId = await context.Ingredients.Where(i => i.Name == ingr.Name).Select(i => i.Id).FirstOrDefaultAsync();

                    if (singleIngredientId != 0)
                    {
                        cocktailIngredient = cocktailIngredientFactory.Create(cocktailId, singleIngredientId, ingr.Value);
                        context.Add(cocktailIngredient);
                    }
                }
            }
            foreach (var ingrName in ingrToRemove)
            {
                var cocktailIngredient = currentIngredients.FirstOrDefault(i => i.Ingredient.Name == ingrName);
                if(cocktailIngredient != null)
                {
                    cocktailIngredient.IsDeleted = true;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
