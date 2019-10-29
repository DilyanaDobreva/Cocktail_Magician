using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    internal static class UserSeed
    {
        public static readonly List<User> userSeed = new List<User>
            {
                new User {
                    Id = "f0476104-41a2-48df-8f15-eb8dc9abbc49",
                    UserName = "origin",
                    Password = "1283eaf1b5d8f1430e47aeb106f598970762618445a450d575aaba48f85b9b39",
                    RoleId = 2
                }
            };
    }
}
