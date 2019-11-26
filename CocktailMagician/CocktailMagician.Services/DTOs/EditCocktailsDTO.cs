using System.Collections.Generic;

namespace CocktailMagician.Services.DTOs
{
    public class EditCocktailsDTO
    {
        public int BarId { get; set; }
        public List<int> CocktailsToAdd { get; set; }
        public List<int> CocktailsToRemove { get; set; }

    }
}
