using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Mapper
{
    public static class AddressDTOMapperExtensions
    {
        public static AddressDTO MapToDTO(this Address address)
        {
            var dto = new AddressDTO
            {
                Name = address.Name,
                CityId = address.CityId,
                CityName = address.City?.Name,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
            return dto;
        }
    }
}
