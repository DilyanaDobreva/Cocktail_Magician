using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class CocktailDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<CocktailIngredientDTO> Ingredients { get; set; }
        public IEnumerable<BarInListDTO> Bars { get; set; }
        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }
}
