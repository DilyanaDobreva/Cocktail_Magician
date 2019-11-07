﻿using CocktailMagician.Services.DTOs;
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
                ImageURL = cocktail.ImageURL
            };
            return vm;
        }
        public static IngredientBasicViewModel MapToViewModel(this IngredientBasicDTO ingredient)
        {
            var vm = new IngredientBasicViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Unit = ingredient.Unit
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
                ImageURL = cocktail.ImageURL,
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
                ImageURL = cocktail.ImageURL,
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
        public static CocktailReviewDTO MapToViewModel(this CocktailReviewDTO review)
        {
            var vm = new CocktailReviewDTO()
            {
                Comment = review.Comment,
                Rating = review.Rating,
                UserName = review.UserName,
                IsDeleted = review.IsDeleted
            };
            return vm;
        }
    }
}

