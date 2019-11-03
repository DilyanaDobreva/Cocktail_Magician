using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class AddBarViewModel
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public AddressViewModel Address { get; set; }
        public List<SelectListItem> AllCities { get; set; }
    }
}
