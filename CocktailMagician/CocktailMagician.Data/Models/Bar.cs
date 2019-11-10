using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Bar
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int AddressId { get; set; }
        [Required]
        public Address Address { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        [Url]
        [Required]
        public string ImageUrl { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
        public bool IsDeleted { get; set; }
    }
}
