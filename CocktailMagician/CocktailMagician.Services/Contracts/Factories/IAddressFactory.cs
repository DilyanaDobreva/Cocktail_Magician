using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IAddressFactory
    {
        Address Create(string name, int cityId, double latitude, double longitude);
    }
}
