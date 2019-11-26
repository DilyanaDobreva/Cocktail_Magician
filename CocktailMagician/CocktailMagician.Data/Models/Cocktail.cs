using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CocktailMagician.Data.Common;

namespace CocktailMagician.Data.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Cocktail name must be less than 50 symbols")]
        public string Name { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
    }
}

