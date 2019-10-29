using System;
using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string userName, string password, int role);
    }
}
