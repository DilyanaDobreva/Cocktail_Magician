using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailsListViewModel
    {
        public CocktailsListViewModel()
        {
            Paging = new Paging();
        }
        public IEnumerable<CocktailInListViewModel> AllCocktails { get; set; }
        public Paging Paging { get; set; } 
    }
}
