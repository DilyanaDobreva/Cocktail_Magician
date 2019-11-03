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
    }
}
