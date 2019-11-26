using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;

namespace CocktailMagician.Services.Factories
{
    public class BarFactory : IBarFactory
    {
        public Bar Create(string name, string imageURL,string phoneNumber,  AddressDTO address)
        {
            var bar = new Bar
            {
                Name = name,
                Address = address.MapFromDTO(),
                ImagePath = imageURL,
                PhoneNumber = phoneNumber
            };

            return bar;
        }
    }
}
