using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task Add(string name, Dictionary<string, int> ingredientsAndQuantities);
        Task Delete(int id);
        Task<Cocktail> Get(int id);
        Task<List<CocktailInListDTO>> GetAllDTO();
        Task AddIngredient(int cocktailId, int ingredientId, int quantity);
        Task RemoveIngredient(int cocktailId, int ingredientId);
    }
}
