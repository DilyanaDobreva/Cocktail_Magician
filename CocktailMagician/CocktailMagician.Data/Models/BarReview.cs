using System;
namespace CocktailMagician.Data.Models
{
    public class BarReview : Review
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
