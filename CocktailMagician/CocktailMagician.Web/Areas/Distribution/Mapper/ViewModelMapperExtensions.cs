using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using CocktailMagician.Web.Areas.Distribution.Models.City;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using CocktailMagician.Web.Areas.Distribution.Models.Ingredients;
using System.Linq;

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
                ImagePath = cocktail.ImagePath,
                AverageRating = cocktail.AverageRating
            };
            vm.AverageRating = cocktail.AverageRating;
            return vm;
        }
        public static IngredientBasicViewModel MapToViewModel(this IngredientBasicDTO ingredient)
        {
            var vm = new IngredientBasicViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Unit = ingredient.Unit,
                CanDelete = ingredient.CanDelete
            };

            return vm;
        }
        public static BarInListViewModel MapToViewModel(this BarInListDTO bar)
        {
            var vm = new BarInListViewModel
            {
                Id = bar.Id,
                Name = bar.Name,
                ImagePath = bar.ImagePath,
                Address = bar.Address,
                City = bar.City
            };
            vm.AverageRating = bar.AverageRating;
            return vm;
        }
        public static CityViewModel MapToViewModel(this CityDTO city)
        {
            var dto = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name
            };
            return dto;
        }
        public static CocktailDetailsViewModel MapToViewModel (this CocktailDetailsDTO cocktail)
        {
            var vm = new CocktailDetailsViewModel
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImageURL = cocktail.ImagePath,
                Ingredients = cocktail.Ingredients.Select(i => i.MapToViewModel()),
                Bars = cocktail.Bars?.Select(b => b.MapToViewModel())
            };
            vm.ListedIngredients = string.Join(", ", vm.Ingredients.Select(i => i.Name));

            return vm;
        }
        public static EditCocktailViewModel MapToEditViewModel(this CocktailDetailsDTO cocktail)
        {
            var vm = new EditCocktailViewModel
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImagePath = cocktail.ImagePath,
                IngredientsQuantity = cocktail.Ingredients.Select(ci => ci.MapToViewModel()).ToList(),
            };
            return vm;
        }
        public static CocktailIngredientViewModel MapToViewModel(this CocktailIngredientDTO dto)
        {
            var vm = new CocktailIngredientViewModel
            {
                Name = dto.Name,
                Value = dto.Value,
                Unit = dto.Unit
            };
            return vm;
        }
        public static BarReviewViewModel MapToViewModel(this BarReviewDTO review)
        {
            var vm = new BarReviewViewModel()
            {
                Comment = review.Comment,
                Rating = review.Rating,
                UserName = review.UserName,
                IsDeleted = review.IsDeleted
            };
            return vm;
        }
        public static ReviewViewModel MapToViewModel(this ReviewDTO review)
        {
            var vm = new ReviewViewModel()
            {
                Comment = review.Comment,
                Rating = review.Rating,
                UserName = review.UserName,
            };
            return vm;
        }
        public static BarDetailsViewModel MapToViewModel(this BarDetailsDTO bar)
        {
            var vm = new BarDetailsViewModel
            {
                Id = bar.Id,
                Name = bar.Name,
                ImagePath = bar.ImagePath,
                PhoneNumber = bar.PhoneNumber,
                AverageRating = bar.AverageRating,
                Address = bar.Address.MapToViewModel(),
                Cocktails = bar.Cocktails.Select(c => c.MapToViewModel()),
                HasReviews = bar.HasReviews
            };
            return vm;
        }
        public static AddressViewModel MapToViewModel(this AddressDTO address)
        {
            var vm = new AddressViewModel
            {
                Address = address.Name,
                CityId = address.CityId,
                CityName = address.CityName,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };
            return vm;
        }
        public static AddBarViewModel MapToViewModel(this BarToEditDTO bar)
        {
            var dto = new AddBarViewModel
            {
                Name = bar.Name,
                ImagePath = bar.ImagePath,
                PhoneNumber = bar.PhoneNumber,
                Address = bar.Address.MapToViewModel()
            };
            return dto;
        }
    }
}

