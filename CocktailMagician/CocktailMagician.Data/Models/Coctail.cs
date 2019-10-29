using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class Coctail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BarCoctail> Bars { get; set; }
        public ICollection<CoctailIngredient> Ingredients { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}

