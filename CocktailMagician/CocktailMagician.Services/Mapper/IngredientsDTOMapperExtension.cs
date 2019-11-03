using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Mapper
{
    public static class IngredientsDTOMapper
    {
        public static IngredientBasicDTO MapToDTO(this Ingredient ingredient)
        {
            var dto = new IngredientBasicDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Unit = ingredient.Unit
            };
            return dto;
        }
    }
}
