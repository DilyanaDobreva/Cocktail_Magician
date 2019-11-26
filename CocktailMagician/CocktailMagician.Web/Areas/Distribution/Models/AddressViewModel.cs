using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.Distribution.Models
{
    public class AddressViewModel
    {
        [Required]
        public string Address { get; set; }
        public string CityName { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
