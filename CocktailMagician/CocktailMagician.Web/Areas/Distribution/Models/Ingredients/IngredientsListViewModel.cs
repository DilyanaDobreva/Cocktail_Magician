using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Ingredients
{
    public class IngredientsListViewModel
    {
        public IngredientsListViewModel()
        {
            Ingredients = new List<IngredientBasicViewModel>();
        }
        public IEnumerable<IngredientBasicViewModel> Ingredients { get; set; }
    }
}
