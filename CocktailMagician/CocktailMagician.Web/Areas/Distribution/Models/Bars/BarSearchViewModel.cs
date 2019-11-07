using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class BarSearchViewModel
    {
        public string NameKey { get; set; }
        public int? MinRating { get; set; }
        public int? CityId { get; set; }
        public List<SelectListItem> AllCities { get; set; }
        public IEnumerable<BarInListViewModel> Result { get; set; }
    }
}
