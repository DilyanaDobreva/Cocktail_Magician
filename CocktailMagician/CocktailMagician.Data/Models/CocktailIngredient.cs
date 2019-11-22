using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class CocktailIngredient
    {
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        [Required]
        [Range(0.1, 10)]
        public double Quatity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
