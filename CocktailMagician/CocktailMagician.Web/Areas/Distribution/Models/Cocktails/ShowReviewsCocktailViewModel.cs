using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class ShowReviewsCocktailViewModel
    {
        public string CocktailName { get; set; }
        public int CocktailId { get; set; }
        public IEnumerable<CocktailReviewViewModel> CocktailReviews { get; set; }
    }
}
