using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class ReviewDTO
    {
            public string Comment { get; set; }
            public int? Rating { get; set; }
            public string UserName { get; set; }
    }
}
