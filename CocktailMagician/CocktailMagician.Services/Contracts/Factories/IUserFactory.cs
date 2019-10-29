using System;
using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string name, string password, int role);
    }
}
