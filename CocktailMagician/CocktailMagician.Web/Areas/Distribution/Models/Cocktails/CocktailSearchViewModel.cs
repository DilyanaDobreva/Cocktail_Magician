using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailSearchViewModel
    {
        public string NameKey { get; set; }
        public int? MinRating { get; set; }
        public int? IngredientId { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public IEnumerable<CocktailInListViewModel> Result { get; set; }
    }
}
