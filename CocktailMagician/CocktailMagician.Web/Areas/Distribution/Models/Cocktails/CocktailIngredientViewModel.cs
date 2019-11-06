using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailIngredientViewModel
    {
        [Range(1,300)]
        public int Value { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
