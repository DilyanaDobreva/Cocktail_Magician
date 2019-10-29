using System;
namespace CocktailMagician.Data.Models
{
    public class CocktailIngredient
    {
        public int CocktailId { get; set; }
        public Cocktail Coctail { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
