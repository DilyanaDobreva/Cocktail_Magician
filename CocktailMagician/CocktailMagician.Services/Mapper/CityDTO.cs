using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Mapper
{
    public static class AddressMapperExtension
    {
        public static CityDTO MapToDTO(this City city)
        {
            var dto = new CityDTO
            {
                Id = city.Id,
                Name = city.Name
            };
            return dto;
        }
    }
}
