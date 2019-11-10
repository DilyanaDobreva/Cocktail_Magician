using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class EditBarsViewModel
    {
        public EditBarsViewModel()
        {
            BarsToAdd = new List<int>();
            BarsToRemove = new List<int>();
        }
        public string CocktailName { get; set; }
        public List<SelectListItem> AllOtherBars { get; set; }
        public List<SelectListItem> BarsOfCocktail { get; set; }
        public List<int> BarsToAdd { get; set; }
        public List<int> BarsToRemove { get; set; }
    }
}
