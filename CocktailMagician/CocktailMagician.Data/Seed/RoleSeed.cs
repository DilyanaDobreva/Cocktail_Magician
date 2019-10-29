using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    internal static class RoleSeed
    {
        public static readonly List<Role> roleSeed = new List<Role>
            {
                new Role() {Id = 1, Name = "user"},
                new Role() {Id = 2, Name = "admin"}
            };
    }
}
