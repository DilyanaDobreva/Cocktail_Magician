using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class AddressFactory : IAddressFactory
    {
        public Address Create(string name, int cityId, double latitude, double longitude)
        {
            var address = new Address
            {
                Name = name,
                CityId = cityId,
                Latitude = latitude,
                Longitude = longitude
            };
            return address;
        }
    }
}
