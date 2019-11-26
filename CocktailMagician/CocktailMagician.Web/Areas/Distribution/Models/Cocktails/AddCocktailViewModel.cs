using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class AddCocktailViewModel
    {
        public AddCocktailViewModel()
        {
            this.IngredientsQuantity = new List<CocktailIngredientViewModel>();
        }
        [Required]
        [MaxLength(50, ErrorMessage ="Name must be less than 50 symbols")]
        public string Name { get; set; }
        [Required]
        [BindProperty]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        //[Url]
        //[Required]
        //public string ImageURL { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        [Required]
        public List<string> CocktilIngredients { get; set; }
        public List<CocktailIngredientViewModel> IngredientsQuantity { get; set; }
    }
}
