using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICityServices
    {
        Task AddCity(string name);
        Task<City> GetCity(int id);
        Task<List<CityDTO>> GetAllCities();
    }
}
