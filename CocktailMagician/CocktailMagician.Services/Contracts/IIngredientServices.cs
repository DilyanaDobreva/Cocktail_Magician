using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientServices
    {
        Task<IngredientBasicDTO> AddAsync(string name, string unit);
        //Task<Ingredient> GetAsync(int id);
        //Task EditAsync(int id, string newName);
        Task DeleteAsync(int id);
        Task<List<IngredientBasicDTO>> GetAllDTOAsync();
        Task<List<IngredientBasicDTO>> GetAllPagedDTOAsync(int itemsPerPage, int currentPage);
        Task<List<IngredientBasicDTO>> GetAllNotIncludedDTOAsync(int cocktailId);
        Task<int> AllIngredientsCountAsync();
    }
}
