using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models.Bars
{
    public class AddressViewModel
    {
        public int CityId { get; set; }
        [Required]
        public string AddressName { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
