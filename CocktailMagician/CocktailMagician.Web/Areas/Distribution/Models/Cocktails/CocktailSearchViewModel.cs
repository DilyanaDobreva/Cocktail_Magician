using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailSearchViewModel
    {
        public CocktailSearchViewModel()
        {
            Paging = new Paging();
        }

        public string NameKey { get; set; }
        public int? MinRating { get; set; }
        public int? IngredientId { get; set; }
        public Paging Paging { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public IEnumerable<CocktailInListViewModel> Result { get; set; }
    }
}
