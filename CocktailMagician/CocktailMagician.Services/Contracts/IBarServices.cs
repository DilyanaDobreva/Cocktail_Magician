using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IBarServices
    {
        Task AddAsync(string name, string imageURL, string phoneNumber, AddressDTO address);
        Task<List<BarInListDTO>> GetAllDTOAsync(int itemsPerPage, int currentPage);
        Task<BarDetailsDTO> GetDetailedDTOAsync(int id);
        Task<string> GetNameAsync(int id);
        Task<List<CocktailBasicDTO>> GetPresentCocktailsAsync(int id);
        Task<List<CocktailBasicDTO>> NotPresentCocktailsAsync(int id);
        Task AddCocktailsAsync(int barId, List<int> cocktailsToAdd);
        Task RemoveCocktailsAsync(int barId, List<int> cocktailsToRemove);
        Task<BarToEditDTO> GetBarToEditDTOAsync(int id);
        Task EditAsync(BarToEditDTO newBarInfo);
        Task DeleteAsync(int id);
        Task<List<BarInListDTO>> SearchAsync(string name, int? cityId, int? minRating);
        Task<int> AllBarsCountAsync();
    }
}
