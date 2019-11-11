using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailIngredientViewModel
    {
        [Required]
        [Range(0.1,10)]
        public double Value { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
