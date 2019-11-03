﻿using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IIngredientFactory
    {
        Ingredient Create(string name, string unit);
    }
}