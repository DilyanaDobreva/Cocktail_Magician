using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class EditCocktailsViewModel
    {
        public EditCocktailsViewModel()
        {
            CocktailsToAdd = new List<int>();
            CocktailsToRemove = new List<int>();
        }
        public string BarName { get; set; }
        public IEnumerable<SelectListItem> PresentCocktails { get; set; }
        public IEnumerable<SelectListItem> NotPresentCocktails { get; set; }
        public List<int> CocktailsToAdd { get; set; }
        public List<int> CocktailsToRemove { get; set; }
    }
}
