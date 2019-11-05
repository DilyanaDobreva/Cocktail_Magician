﻿using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailIngredientViewModel
    {
        [Range(0,1000)]
        public int Value { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}