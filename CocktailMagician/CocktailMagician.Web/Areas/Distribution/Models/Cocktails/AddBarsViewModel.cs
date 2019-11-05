using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class AddBarsViewModel
    {
        public string CocktailName { get; set; }
        public List<SelectListItem> AllBars { get; set; }
        public List<int> SelectedBars { get; set; }
    }
}
