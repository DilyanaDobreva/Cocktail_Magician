using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Bar> Bars { get; set; }
        public bool IsDeleted { get; set; }
    }
}
