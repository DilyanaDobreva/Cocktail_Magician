using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class CocktailSearchDTO
    {
        public string NameKey { get; set; }
        public int? MinRating { get; set; }
        public int? IngredientId { get; set; }

    }
}
