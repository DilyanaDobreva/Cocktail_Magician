﻿using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BarCocktail> Cocktails { get; set; }
        public bool IsDeleted { get; set; }
    }
}
