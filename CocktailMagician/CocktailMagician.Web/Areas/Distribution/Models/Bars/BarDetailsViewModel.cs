using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class BarDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public AddressViewModel Address { get; set; }
        public IEnumerable<CocktailInListViewModel> Cocktails { get; set; }
    }
}
