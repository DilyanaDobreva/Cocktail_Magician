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
        Task<UserDTO> FindUserDTOAsync(string name);
        Task<User> FindUserAsync(string name);
        Task<UserDTO> LoginAsync(string username, string password);
        Task UpdateUserAsync(string id, string password,string newPassword, int roleId);
        Task<UserDTO> GetUserInfoAsync(string name);
        Task<BannDTO> BanAsync(string reason, User member);
        Task DeleteBan(Bann bann);
        Task<List<UserDTO>> GetListOfUsersDTO();
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<bool> IsBannedAsync(string userName);
    }
}
