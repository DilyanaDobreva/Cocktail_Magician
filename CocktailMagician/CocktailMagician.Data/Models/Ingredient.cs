using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BarCoctail> Coctails { get; set; }
        public bool IsDeleted { get; set; }
    }
}
