using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Mapper
{
    public static class BarDTOMapprerExtensions
    {
        public static Address MapFromDTO(this AddressDTO address)
        {
            var addressModel = new Address
            {
                Name = address.Name,
                CityId = address.CityId,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
            return addressModel;
        }
        public static BarInListDTO MapToDTO(this Bar bar)
        {
            var dto = new BarInListDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                ImageURL = bar.ImageUrl
            };
            return dto;
        }
        
    }
}
