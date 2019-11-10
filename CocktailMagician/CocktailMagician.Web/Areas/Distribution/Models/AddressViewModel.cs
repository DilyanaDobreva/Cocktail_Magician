using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models
{
    public class AddressViewModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string CityName { get; set; }
        public int CityId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
