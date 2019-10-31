using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
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
        public async Task<BannDTO> BanAsync(string reason, User user)
        {
            var date = DateTime.Now;

            if (user.Bann != null)
            {
                var existingBan = await context.Banns.Include(m => m.User).FirstOrDefaultAsync(m => m.UserId == user.Id);
                this.context.Banns.Remove(existingBan);
            }
            var bann = bannFactory.CreateBan(reason, date.AddDays(30), user);
            this.context.Banns.Add(bann);
            await this.context.SaveChangesAsync();
            return bann.MapToDTO();
        }
        public async Task DeleteBan(Bann bann)
        {
            var findBan = context.Banns
                .FirstOrDefault(b => b.Id == bann.Id);
            findBan.IsDeleted = true;
            await this.context.SaveChangesAsync();
        }
        public Task<User> FindUserAsync(string name)
        {
            var findUser = context.Users
                .FirstOrDefaultAsync(u => u.UserName == name);
            return findUser;
        }
        public async Task<UserDTO> FindUserDTOAsync(string name)
        {
            var findUser = await context.Users
                .Include(u => u.Bann)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == name);
            return findUser.MapToDTO();
        }
        public async Task<List<UserDTO>> GetListOfUsersDTO()
        {
            var users = await context.Users
                .Include(u => u.Role)
                .Include(u => u.Bann)
                .ToListAsync();
            var listOfDTO = users.Select(m => m.MapToDTO()).ToList();
            return listOfDTO;
        }
        public async Task<UserDTO> GetUserInfoAsync(string id)
        {
            var users = await context.Users
                .Include(m => m.Role)
                .Include(m => m.Bann)
                .FirstAsync(m => m.Id == id);
            return users.MapToDTO();
        }
        public async Task<UserDTO> LoginAsync(string username, string password)
        {
            password = hasher.Hasher(password);
            var user = await context.Users
                .Include(u => u.Role)
                .Include(b => b.Bann)
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                throw new ArgumentException("Invalid Username or Password");
            }
            return user.MapToDTO();

        }
        public async Task<UserDTO> RegisterAdminAsync(string username, string password)
        {
            var findUser = await FindUserAsync(username);
            if (findUser != null)
            {
                throw new ArgumentException("User with this name already exist.");
            }
            var newAdmin = usersFactory.CreateUser(username, password, 2);
            newAdmin.Password = hasher.Hasher(newAdmin.Password);
            this.context.Users.Add(newAdmin);
            await this.context.SaveChangesAsync();
            return newAdmin.MapToDTO();
        }
        public async Task<UserDTO> RegisterUserAsync(string username, string password)
        {
            var findUser = await FindUserAsync(username);
            if (findUser != null)
            {
                throw new ArgumentException("User with this name already exist.");
            }
            var newUser = usersFactory.CreateUser(username, password, 1);
            newUser.Password = hasher.Hasher(newUser.Password);
            this.context.Users.Add(newUser);
            await this.context.SaveChangesAsync();
            var member = await context.Users
                .Include(m => m.Role)
                .FirstOrDefaultAsync(m => m.UserName == username);
            return member.MapToDTO();
        }
        public async Task UpdateUserAsync(string id,string passwrod, string newPassword, int roleId)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (passwrod != user.Password)
            {
                throw new ArgumentException("Current Password is incorrect.");
            }
            if (user.Password == newPassword)
            {
                throw new ArgumentException("New password cannot be the same as old password.");
            }
            if (newPassword != null)
            {
                user.Password = hasher.Hasher(newPassword);
                await this.context.SaveChangesAsync();
            }
            if (roleId != user.RoleId)
            {
                user.RoleId = roleId;
                await this.context.SaveChangesAsync();
            }
        }
        public Task<bool> IsBannedAsync(string userName)
        {
            return context.Banns
                .Include(m => m.User)
                .AnyAsync(u => u.User.UserName == userName);
        }
        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            var roles = await context.Roles.ToListAsync();

            var rolesDTO = roles.Select(r => r.MapToDTO());
            return rolesDTO;
        }
    }
}
