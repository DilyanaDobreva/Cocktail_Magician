using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Bar
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = (5))]
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<BarCoctail> Coctails { get; set; }
        public string Image { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public bool IsDeleted { get; set; }
    }
}
