using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class RemoveBarsViewModel
    {
        public string CocktailName { get; set; }
        public List<SelectListItem> BarsOfCocktail { get; set; }
        public List<int> BarsToRemove { get; set; }
    }
}
