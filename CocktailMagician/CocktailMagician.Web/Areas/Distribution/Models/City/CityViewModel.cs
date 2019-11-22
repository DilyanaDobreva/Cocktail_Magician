using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.City
{
    public class CityViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
