using System;
namespace CocktailMagician.Data.Models
{
    public class CoctailIngredient
    {
        public int CoctailId { get; set; }
        public Coctail Coctail { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
