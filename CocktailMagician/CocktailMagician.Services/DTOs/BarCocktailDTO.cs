using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class BarCocktailDTO
    {
        public string BarName { get; set; }
        public string BarAddress { get; set; }
        public IEnumerable<CocktailBasicDTO> Cocktails { get; set; }
    }
}
