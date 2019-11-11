using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;

namespace CocktailMagician.Services.Factories
{
    public class BarFactory : IBarFactory
    {
        public Bar Create(string name, string imageURL, AddressDTO address)
        {
            var bar = new Bar
            {
                Name = name,
                Address = address.MapFromDTO(),
                ImageUrl = imageURL
            };

            return bar;
        }
    }
}
