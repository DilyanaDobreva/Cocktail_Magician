using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class CityFactory : ICityFactory
    {
        public City Create(string name)
        {
            var city = new City
            {
                Name = name
            };
            return city;
        }
    }
}
