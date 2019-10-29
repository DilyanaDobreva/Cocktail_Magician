using System;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class UserFactory : IUserFactory
    {
        

        public User CreateUser(string userName, string password, int role)
        {
            if (userName.Length < 5 || password.Length < 5)
            {
                throw new ArgumentException("User name and password cannot be less than 5 symbols.");
            }
            return new User(userName, password, role);
        }
    }
}
