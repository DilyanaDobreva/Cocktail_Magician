using System;
namespace CocktailMagician.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}
