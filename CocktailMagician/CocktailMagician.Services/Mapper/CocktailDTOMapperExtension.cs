using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Mapper
{
    public static class CocktailDTOMapperExtension
    {
        public static CocktailInListDTO MapToDTO(this Cocktail cocktail)
        {
            var dto = new CocktailInListDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name
            };

            return dto;
        }
    }
}
