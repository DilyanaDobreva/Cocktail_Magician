using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class AddBarViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Url]
        [Required]
        public string ImageURL { get; set; }
        public AddressViewModel Address { get; set; }
        public List<SelectListItem> AllCities { get; set; }
    }
}
