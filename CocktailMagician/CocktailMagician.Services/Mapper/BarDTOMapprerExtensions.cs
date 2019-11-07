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
        public static BarInListDTO MapToDTO(this Bar bar)
        {
            var dto = new BarInListDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                ImageURL = bar.ImageUrl,
                Address = bar.Address.Name,
                City = bar.Address.City.Name
            };
            return dto;
        }
        public static BarBasicDTO MapToBasicDTO(this Bar bar)
        {
            var dto = new BarBasicDTO
            {
                Id = bar.Id,
                Name = bar.Name
            };
            return dto;
        }
        public static BarDetailsDTO MapToDetailsDTO(this Bar bar)
        {
            var dto = new BarDetailsDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                ImageURL = bar.ImageUrl,
                Address = bar.Address.MapToDTO() ,
                Cocktails = bar.BarCocktails.Where(b => b.IsDeleted == false).Select(bc => bc.Cocktail.MapToDTO())
            };
            return dto;
        }
        public static BarToEditDTO MapToEditDTO(this Bar bar)
        {
            var dto = new BarToEditDTO
            {
                Name = bar.Name,
                ImageURL = bar.ImageUrl,
                Id = bar.Id,
                Address = bar.Address.MapToDTO()
            };
            return dto;
        }
    }
}
