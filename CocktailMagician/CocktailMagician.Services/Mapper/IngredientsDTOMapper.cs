using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Mapper
{
    public static class IngredientsDTOMapper
    {
        public static IngredientDTO MapToDTO(this Ingredient ingredient)
        {
            var dto = new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
            return dto;
        }
    }
}
