using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public IEnumerable<CocktailIngredientViewModel> Ingredients { get; set; }
        public IEnumerable<BarInListViewModel> Bars { get; set; }
        public string ListedIngredients { get; set; }
    }
}
