using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Contracts
{
    public interface IUserServices
    {
        Task<UserDTO> RegisterUserAsync(string username, string password);
        Task<UserDTO> RegisterAdminAsync(string username, string password);
        Task<UserDTO> LoginAsync(string username, string password);
        Task UpdateUserAsync(string id, string newPassword);
        Task<UserDTO> GetUserAsync(string name);
        Task<List<Bann>> FindExpiredBans();
        Task<Bann> BanUserAsync(string reason, User member);
        Task DeleteBan(Bann bann);
        Task<List<UserDTO>> GetListOfUsersDTO();
    }
}
