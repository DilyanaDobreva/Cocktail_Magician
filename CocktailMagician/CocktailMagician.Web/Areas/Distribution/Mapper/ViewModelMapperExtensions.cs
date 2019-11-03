using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using CocktailMagician.Web.Areas.Distribution.Models.Ingredients;

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
        public static IngredientBasicViewModel MapToViewModel(this IngredientBasicDTO ingredient)
        {
            var vm = new IngredientBasicViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };

            return vm;
        }
        public static BarInListViewModel MapToViewModel(this BarInListDTO bar)
        {
            var vm = new BarInListViewModel
            {
                Id = bar.Id,
                Name = bar.Name,
                ImageURL = bar.ImageURL,
                Address = bar.Address,
                City = bar.City
            };
            return vm;
        }

    }
}

