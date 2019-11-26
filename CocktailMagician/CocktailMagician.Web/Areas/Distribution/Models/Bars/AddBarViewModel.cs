using Microsoft.AspNetCore.Http;
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
        [Required]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public AddressViewModel Address { get; set; }
        public List<SelectListItem> AllCities { get; set; }
    }
}
