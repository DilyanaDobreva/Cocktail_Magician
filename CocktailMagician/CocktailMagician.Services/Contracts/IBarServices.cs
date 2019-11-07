using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IBarServices
    {
        Task Add(string name, string imageURL, AddressDTO address);
        Task<List<BarInListDTO>> GetAllDTO();
        Task<List<BarBasicDTO>> GetAllNotIncludedDTO(int cocktailId);
        Task<List<BarBasicDTO>> GetBarsOfCocktail(int cocktailId);
        Task<BarDetailsDTO> GetDetailedDTO(int id);
        Task<string> GetName(int id);
        Task<List<CocktailBasicDTO>> GetPresentCocktails(int id);
        Task<List<CocktailBasicDTO>> NotPresentCocktails(int id);
        Task EditCocktails(EditCocktailsDTO editCocktails);
        Task<BarToEditDTO> GetBarToEditDTO(int id);
        Task Edit(BarToEditDTO newBarInfo);
        Task Delete(int id);
    }
}
