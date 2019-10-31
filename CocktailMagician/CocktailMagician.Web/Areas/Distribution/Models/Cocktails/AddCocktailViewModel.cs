using CocktailMagician.Web.Areas.Distribution.Models.Ingredients;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class AddCocktailViewModel
    {
        public string Name { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public Dictionary<int, int> IngredientsQuantity { get; set; }
    }
}
