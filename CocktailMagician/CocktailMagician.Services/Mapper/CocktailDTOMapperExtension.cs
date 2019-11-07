using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Linq;

namespace CocktailMagician.Services.Mapper
{
    public static class CocktailDTOMapperExtension
    {
        public static CocktailInListDTO MapToDTO(this Cocktail cocktail)
        {
            var dto = new CocktailInListDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImageURL = cocktail.ImageUrl
            };

            return dto;
        }
        public static CocktailDetailsDTO MapToDetailsDTO(this Cocktail cocktail)
        {
            var dto = new CocktailDetailsDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImageURL = cocktail.ImageUrl,
                Ingredients = cocktail.CocktailIngredients.Where(i => i.IsDeleted == false).Select(i => i.MapToDTO()),
                Bars = cocktail.BarCocktails?.Where(b => b.IsDeleted == false).Select(b => b.Bar.MapToDTO())
            };
            return dto;
        }
        public static CocktailIngredientDTO MapToDTO( this CocktailIngredient ci)
        {
            var dto = new CocktailIngredientDTO
            {
                Name = ci.Ingredient.Name,
                Unit = ci.Ingredient.Unit,
                Value = ci.Quatity
            };
            return dto;
        }
        public static CocktailBasicDTO MapToBasicDTO(this Cocktail cocktail)
        {
            var dto = new CocktailBasicDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name
            };
            return dto;
        }
    }
}

