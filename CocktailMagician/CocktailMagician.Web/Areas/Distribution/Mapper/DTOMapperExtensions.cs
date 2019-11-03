using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;

namespace CocktailMagician.Web.Areas.Distribution.Mapper
{
    public static class DTOMapperExtensions
    {
        public static CocktailIngredientDTO MapToDTO(this CocktailIngredientViewModel vm)
        {
            var dto = new CocktailIngredientDTO
            {
                Name = vm.Name,
                Value = vm.Value
            };
            return dto;
        }
        public static AddressDTO MapToDTO(this AddressViewModel address)
        {
            var dto = new AddressDTO
            {
                Name = address.AddressName,
                CityId = address.CityId,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
            return dto;
        }
    }
}
