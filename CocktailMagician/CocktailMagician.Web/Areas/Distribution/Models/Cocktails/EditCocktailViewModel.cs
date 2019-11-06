using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class EditCocktailViewModel
    {
        public EditCocktailViewModel()
        {
            this.IngredientsQuantity = new List<CocktailIngredientViewModel>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 symbols")]
        public string Name { get; set; }
        [Url]
        public string ImageURL { get; set; }
        public List<string> ListCurrentIngredients { get; set; }
        public List<SelectListItem> AllNotIncludedIngredients { get; set; }
        public List<SelectListItem> CurrentCocktailIngredients { get; set; }
        public List<string> CocktilNewIngredients { get; set; }
        public List<string> IngredientsToRemove { get; set; }
        public List<CocktailIngredientViewModel> IngredientsQuantity { get; set; }

    }
}
