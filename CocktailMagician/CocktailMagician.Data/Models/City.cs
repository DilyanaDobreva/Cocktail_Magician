using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
