using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICityServices
    {
        Task<CityDTO> Add(string name);
        Task<City> Get(int id);
        Task<List<CityDTO>> GetAllDTO();
    }
}
