using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Linq;

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
    }
}
