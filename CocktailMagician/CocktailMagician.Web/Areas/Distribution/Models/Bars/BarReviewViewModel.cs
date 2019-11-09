using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class BarReviewViewModel
    {
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public BarDetailsViewModel Bar { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
