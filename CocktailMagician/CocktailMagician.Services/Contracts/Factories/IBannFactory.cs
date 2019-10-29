using System;
using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IBannFactory
    {
        Bann CreateBan(string reason, DateTime enDateTime, User member);
    }
}
