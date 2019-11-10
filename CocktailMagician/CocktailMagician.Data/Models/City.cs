using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
