using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class BarDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public double? AverageRating { get; set; }
        public AddressDTO Address { get; set; }
        public IEnumerable<CocktailInListDTO> Cocktails { get; set; }
    }
}
