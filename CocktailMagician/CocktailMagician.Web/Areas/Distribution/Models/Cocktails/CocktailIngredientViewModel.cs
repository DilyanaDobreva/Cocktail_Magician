using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailIngredientViewModel
    {
        [Range(0,1000)]
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
