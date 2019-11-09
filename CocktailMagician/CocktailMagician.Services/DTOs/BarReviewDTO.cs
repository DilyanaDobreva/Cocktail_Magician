using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class BarReviewDTO
    {
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string UserName { get; set; }
        public BarBasicDTO Bar { get; set; }
        public bool IsDeleted { get; set; }
    }
}
