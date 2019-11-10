using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public bool IsDeleted { get; set; }
    }
}
