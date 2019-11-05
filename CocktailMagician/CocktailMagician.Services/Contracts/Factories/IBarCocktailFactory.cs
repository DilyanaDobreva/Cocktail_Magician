using CocktailMagician.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IBarCocktailFactory
    {
        BarCocktail Create(int barId, int cocktailId);
    }
}
