using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using System.Collections.Generic;

namespace CocktailMagician.Web.Models
{
    public class BarAndCoctailInListViewModel
    {
        public List<BarInListViewModel> ListOfBars { get; set; }
        public List<CocktailInListViewModel> ListOfCocktails { get; set; }

    }
}
