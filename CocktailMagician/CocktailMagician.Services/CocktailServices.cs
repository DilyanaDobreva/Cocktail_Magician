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
using CocktailMagician.Services.SearchFilter;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CocktailMagicianDb context;
        private readonly ICocktailFactory cocktailFactory;
        private readonly ICocktailIngredientFactory cocktailIngredientFactory;
        private readonly IBarCocktailFactory barCocktailFactory;

        public CocktailServices(CocktailMagicianDb context, ICocktailFactory cocktailFactory, ICocktailIngredientFactory cocktailIngredientFactory,
            IBarCocktailFactory barCocktailFactory)
        {
            this.context = context;
            this.cocktailFactory = cocktailFactory;
            this.cocktailIngredientFactory = cocktailIngredientFactory;
            this.barCocktailFactory = barCocktailFactory;
        }

        public async Task AddAsync(string name, string imageURL, List<CocktailIngredientDTO> ingredientsAndQuantities)
        {
            //TODO To ask if it is neccessary double protection against invalid input

            if (ingredientsAndQuantities == null || ingredientsAndQuantities.Count() == 0)
                throw new InvalidOperationException(OutputConstants.CocktailWithNoIngredients);
            
            if(ingredientsAndQuantities.Any(i => i.Value == 0.0))
                throw new InvalidOperationException(OutputConstants.NoIngredientQuantity);

            if(context.Cocktails.Any(c => c.Name == name && c.IsDeleted == false))
                throw new ArgumentException(OutputConstants.CocktailExists);
            
            
            var cocktail = cocktailFactory.Create(name, imageURL);

            foreach (var ingredient in ingredientsAndQuantities)
            {
                var ingrId = await context.Ingredients.Where(i => i.Name == ingredient.Name).Select(i => i.Id).FirstOrDefaultAsync();
                var cocktailIngredient = cocktailIngredientFactory.Create(cocktail, ingrId, ingredient.Value);
                context.CocktailIngredients.Add(cocktailIngredient);
            }
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if (!await context.Cocktails.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var cocktail = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                .Include(c => c.BarCocktails)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);

            context.BarCocktails.RemoveRange(cocktail.BarCocktails);
            context.CocktailIngredients.RemoveRange(cocktail.CocktailIngredients);

            cocktail.IsDeleted = true;

            await context.SaveChangesAsync();
        }
        public async Task<CocktailDetailsDTO> GetDTOAsync(int id)
        {
            if (!await context.Cocktails.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var cocktailDTO = await context.Cocktails
                .Include(c => c.BarCocktails)
                    .ThenInclude(b => b.Bar)
                        .ThenInclude(b => b.Address)
                            .ThenInclude(a => a.City)
                .Where(c => c.Id == id && c.IsDeleted == false)
                .Select(c => new CocktailDetailsDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageURL = c.ImageUrl,
                    Bars = c.BarCocktails.Select(b => new BarInListDTO
                    {
                        Id = b.Bar.Id,
                        Name = b.Bar.Name,
                        ImageURL = b.Bar.ImageUrl,
                        Address = b.Bar.Address.Name,
                        City = b.Bar.Address.City.Name
                    })
                })
                .FirstOrDefaultAsync();

            cocktailDTO.Ingredients = await context.CocktailIngredients
                .Include(c => c.Ingredient)
                .Where(c => c.CocktailId == id)
                .Select(ci => new CocktailIngredientDTO
                {
                    Name = ci.Ingredient.Name,
                    Unit = ci.Ingredient.Unit,
                    Value = ci.Quatity
                })
                .ToListAsync();

            return cocktailDTO;
        }
        public async Task<List<CocktailInListDTO>> GetAllDTOAsync(int itemsPerPage, int currentPage)
        {

            var list = await context.Cocktails
                .Include(c => c.CocktailReviews)
                .Where(c => c.IsDeleted == false)
                .Select(c => new CocktailInListDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageURL = c.ImageUrl,
                    AverageRating = c.CocktailReviews
                        .Where(r => r.Rating != null)
                        .Select(r => r.Rating)
                        .Average()
                })
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            //To Check K.
            //foreach (var item in list)
            //{
            //    item.AverageRating = await cocktailReviewServices.GetMidRatingAsync(item.Id);
            //}
            return list;
        }
        public async Task AddIngredientAsync(int cocktailId, int ingredientId, int quantity)
        {
            var existing = await context.CocktailIngredients.FirstOrDefaultAsync(ci => ci.IngredientId == ingredientId && ci.CocktailId == cocktailId);
            if (existing != null)
            {
                existing.Quatity = quantity;
            }
            else
            {
                var newIngredient = cocktailIngredientFactory.Create(cocktailId, ingredientId, quantity);
                context.CocktailIngredients.Add(newIngredient);
            }

            await context.SaveChangesAsync();
        }
        public async Task RemoveIngredientAsync(int cocktailId, int ingredientId)
        {
            var ingredient = await context.CocktailIngredients
                .FirstOrDefaultAsync(ci => ci.IngredientId == ingredientId && ci.CocktailId == cocktailId);

            if (ingredient == null)
            {
                throw new ArgumentException(OutputConstants.IngredientNotFound);
            }

            context.CocktailIngredients.Remove(ingredient);
            await context.SaveChangesAsync();
        }
        public Task<string> GetNameAsync(int id)
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
                if (barCocktail == null)
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
                    context.BarCocktails.Remove(barCocktail);
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task EditIngredientsAsync(int cocktailId, List<CocktailIngredientDTO> newIngredients, List<string> ingrToRemove)
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

                if (cocktailIngredient == null)
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
                if (cocktailIngredient != null)
                {
                    context.CocktailIngredients.Remove(cocktailIngredient);
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task<List<CocktailInListDTO>> SearchAsync(string name, int? ingredientId, int? minRating)
        {
            var resultDTO = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                .Include(c => c.CocktailReviews)
                .Where(c => c.IsDeleted == false)
                .FilterByName(name)
                .FilterByIngredient(ingredientId)
                .FilterByRating(minRating)
                .Select(c => new CocktailInListDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageURL = c.ImageUrl
                })
                .ToListAsync();

            return resultDTO;
        }
        public async Task<int> AllCocktailsCountAsync()
        {
            var count = await context.Cocktails.Where(c => c.IsDeleted == false).CountAsync();
            return count;
        }
        public async Task<List<BarBasicDTO>> GetAllNotIncludedDTOAsync(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                .Where(b => b.IsDeleted == false && !(b.BarCocktails.Any(c => c.CocktailId == cocktailId)))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;
        }
        public async Task<List<BarBasicDTO>> GetBarsOfCocktailAsync(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                .Where(b => b.IsDeleted == false && b.BarCocktails.Any(c => c.CocktailId == cocktailId))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;

        }
        public async Task<bool> DoesNameExist(string name)
        {
            return await context.Cocktails.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.IsDeleted == false);
        }

    }
}
