using System;
namespace CocktailMagician.Data.Models
{
    public class BarCoctail
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public int CoctailId { get; set; }
        public Coctail Coctail { get; set; }
    }
}
