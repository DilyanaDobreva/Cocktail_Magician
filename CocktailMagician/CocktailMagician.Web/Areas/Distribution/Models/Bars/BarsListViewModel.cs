using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class BarsListViewModel
    {
        public BarsListViewModel()
        {
            Paging = new Paging();
        }
        public IEnumerable<BarInListViewModel> AllBars { get; set; }
        public Paging Paging { get; set; }
    }
}
