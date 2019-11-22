using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class BarSearchDTO
    {
        public string NameKey { get; set; }
        public int? MinRating { get; set; }
        public int? CityId { get; set; }

    }
}
