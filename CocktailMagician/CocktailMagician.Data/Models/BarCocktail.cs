using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class BarCocktail
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
