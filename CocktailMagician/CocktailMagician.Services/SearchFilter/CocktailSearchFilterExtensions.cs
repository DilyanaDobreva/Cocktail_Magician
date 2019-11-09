using CocktailMagician.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Services.SearchFilter
{
    public static class CocktailSearchFilterExtensions
    {
        public static IQueryable<Cocktail> FilterByName(this IQueryable<Cocktail> list, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return list;
            }
            var newList = list.Where(b => b.Name.ToLower().Contains(key.ToLower()));
            return newList;
        }

        public static IQueryable<Cocktail> FilterByIngredient(this IQueryable<Cocktail> list, int? ingredientId)
        {
            if (ingredientId == null)
            {
                return list;
            }
            var newList = list.Where(b => b.CocktailIngredients.Any(ci => ci.IngredientId == ingredientId));
            return newList;
        }
        public static IQueryable<Cocktail> FilterByRating(this IQueryable<Cocktail> list, int? minRating)
        {
            if (minRating == null)
            {
                return list;
            }
            //TODO D: Test is this working properly when revews are added
            var newList = list.Where(b => b.CocktailReviews.Any(br => br.Rating != null) && b.CocktailReviews.Where(br => br != null).Average(r => r.Rating) >= minRating);
            return newList;
        }
    }
}
