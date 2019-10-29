using CocktailMagician.Data.Models;
using System;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientServices
    {
        Task Add(string name);
        Task<Ingredient> Get(int id);
        Task Edit(int id, string newName);
        Task Delete(int id);
    }
}
