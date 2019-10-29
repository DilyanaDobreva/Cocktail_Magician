using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class BarCocktail
    {
        [Key]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        [Key]
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
