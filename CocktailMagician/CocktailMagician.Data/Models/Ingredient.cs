using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public bool IsDeleted { get; set; }
    }
}
