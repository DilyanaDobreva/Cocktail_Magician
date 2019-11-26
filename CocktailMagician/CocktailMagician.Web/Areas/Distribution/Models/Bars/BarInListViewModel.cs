namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class BarInListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public double? AverageRating { get; set; }

    }
}
