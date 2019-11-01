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
        public AddCocktailViewModel()
        {
            this.IngredientsQuantity = new List<Coct1>();
        }
        public string Name { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public List<string> CocktilIngredients { get; set; }
        public List<Coct1> IngredientsQuantity { get; set; }
    }

    public class Coct1
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
