using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task Add(string name, string inmageURL, List<CocktailIngredientDTO> ingredientsAndQuantities);
        Task Delete(int id);
        Task<CocktailDetailsDTO> GetDTO(int id);
        Task<List<CocktailInListDTO>> GetAllDTO();
        Task AddIngredient(int cocktailId, int ingredientId, int quantity);
        Task RemoveIngredient(int cocktailId, int ingredientId);
        Task<string> GetName(int id);
        Task AddBarsAsync(int cocktailID, List<int> barsId);
        Task RemoveBarsAsync(int cocktailID, List<int> barsId);
        Task EditIngredients(int cocktailId, List<CocktailIngredientDTO> ci, List<string> ingrToRemove);
    }
}
