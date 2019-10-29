using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}

