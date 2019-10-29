using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Services.Contracts;

namespace CocktailMagician.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CocktailMagicianDb context;

        public CocktailServices(CocktailMagicianDb context)
        {
            this.context = context;
        }

        public async Task Add(string name, Dictionary<string, int> ingredients)
        {
            foreach (var ingredient in ingredients)
            {

            }
        }
    }
}
