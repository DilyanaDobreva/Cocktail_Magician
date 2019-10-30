using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services
{
    public static class OutputConstants
    {
        //Exception Messages
        public const string IngredientNotFound = "Ingredient is not found.";
        public const string CocktailWithNoIngredients = "This cocktail contains no ingredients.";
        public const string CoctailIncludeIngredient = "This ingredient is part of some cocktail and cannot be deleted.";
    }
}
