﻿using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailServices
    {
        Task AddAsync(string name, string inmageURL, List<CocktailIngredientDTO> ingredientsAndQuantities);
        Task DeleteAsync(int id);
        Task<CocktailDetailsDTO> GetDetailedDTOAsync(int id);
        Task<List<CocktailInListDTO>> GetAllDTOAsync(int itemsPerPage, int currentPage);
        //Task AddIngredientAsync(int cocktailId, int ingredientId, int quantity);
        //Task RemoveIngredientAsync(int cocktailId, int ingredientId);
        Task<CocktailInListDTO> GetDTOAsync(int id);
        Task AddBarsAsync(int cocktailID, List<int> barsId);
        Task RemoveBarsAsync(int cocktailID, List<int> barsId);
        Task EditIngredientsAsync(int cocktailId, List<CocktailIngredientDTO> ci, List<string> ingrToRemove);
        Task<List<CocktailInListDTO>> SearchAsync(CocktailSearchDTO dto, int itemsPerPage, int currentPage);
        Task<int> AllCocktailsCountAsync();
        Task<List<BarBasicDTO>> GetBarsOfCocktailAsync(int cocktailId);
        Task<List<BarBasicDTO>> GetAllNotIncludedBarsDTOAsync(int cocktailId);
        Task<bool> DoesNameExist(string name);
        Task<List<IngredientBasicDTO>> GetAllNotIncludedIngredientsDTOAsync(int cocktailId);
        Task<int> SerchResultCountAsync(CocktailSearchDTO dto);
        Task<List<CocktailInListDTO>> GetMostPopular(int number);
    }
}
