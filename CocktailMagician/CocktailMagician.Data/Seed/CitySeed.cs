using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    public static class CitySeed
    {
        public static readonly List<City> cities = new List<City>
        {
            new City
            {
                Id = 1,
                Name = "Sofia"
            },
            new City
            {
                Id = 2,
                Name = "Varna"
            },
            new City
            {
                Id = 3,
                Name = "Plovdiv"
            },
        };
    }
}
