using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;

namespace CocktailMagician.Web.Areas.Distribution.Mapper
{
    public static class ViewModelMapperExtensions
    {
        public static CocktailInListViewModel MapToViewModel(this CocktailInListDTO cocktail)
        {
            var vm = new CocktailInListViewModel
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImageURL = cocktail.ImageURL
            };
            return vm;
        }
        public static IngredientBasicDTO MapToViewModel(this IngredientBasicDTO ingredient)
        {
            var vm = new IngredientBasicDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };

            return vm;
        }
    }
}

