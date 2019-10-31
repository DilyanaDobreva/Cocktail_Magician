using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailsListViewModel
    {
        public IEnumerable<CocktailInListViewModel> AllCocktails { get; set; }
    }
}
