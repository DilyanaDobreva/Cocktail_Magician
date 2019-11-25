using System;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;

namespace CocktailMagician.Services.Factories
{
    public class BannFactory : IBannFactory
    {
        public Bann CreateBan(string reason, DateTime endDateTime, User member)
        {
            return new Bann(reason, endDateTime, member);
        }
    }
}
