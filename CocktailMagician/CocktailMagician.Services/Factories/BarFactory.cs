using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class BarFactory : IBarFactory
    {
        public Bar Create(string name, string imageURL, Address address)
        {
            var bar = new Bar
            {
                Name = name,
                Address = address,
                ImageUrl = imageURL
            };

            return bar;
        }
    }
}
