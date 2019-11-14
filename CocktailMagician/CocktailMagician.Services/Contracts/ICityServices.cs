using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICityServices
    {
        Task<CityDTO> AddAsync(string name);
        Task<List<CityDTO>> GetAllDTOAsync();
    }
}
