using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models;
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
                Value = vm.Value,
                Unit = vm.Unit
            };
            return dto;
        }
        public static AddressDTO MapToDTO(this AddressViewModel address)
        {
            var dto = new AddressDTO
            {
                Name = address.Address,
                CityId = address.CityId,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
            return dto;
        }
        public static BarToEditDTO MapToDTO(this AddBarViewModel bar)
        {
            var dto = new BarToEditDTO
            {
                Name = bar.Name,
                ImagePath = bar.ImagePath,
                Address = bar.Address.MapToDTO()
            };
            return dto;
        }
    }
}
