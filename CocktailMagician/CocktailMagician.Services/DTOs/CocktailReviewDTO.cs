using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class CocktailReviewDTO
    {
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string UserName { get; set; }
        //To be checked from Kris
        public CocktailInListDTO Cocktail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
