using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientServices
    {
        Task Add(string name);
        Task<Ingredient> Get(int id);
        Task Edit(int id, string newName);
        Task Delete(int id);
        Task<List<IngredientDTO>> GetAllDTO();
    }
}
