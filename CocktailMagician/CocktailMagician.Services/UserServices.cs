using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class UserServices : IUserServices
    {
        private readonly CocktailMagicianDb context;
        private readonly IUserFactory usersFactory;
        private readonly IBannFactory bannFactory;
        private readonly IHasher hasher;

        public UserServices(CocktailMagicianDb context, IUserFactory userFactory, IBannFactory bannFactory, IHasher hasher)
        {
            this.context = context;
            this.usersFactory = userFactory;
            this.bannFactory = bannFactory;
            this.hasher = hasher;
        }
        public async Task<Bann> BanUserAsync(string reason, User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBan(Bann bann)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bann>> FindExpiredBans()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDTO>> GetListOfUsersDTO()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> RegisterAdminAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> RegisterUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(string id, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
