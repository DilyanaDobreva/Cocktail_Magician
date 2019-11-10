using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}
