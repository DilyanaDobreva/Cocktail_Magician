using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class CocktailIngredient
    {
        [Key]
        public int CocktailId { get; set; }
        public Cocktail Coctail { get; set; }
        [Key]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
