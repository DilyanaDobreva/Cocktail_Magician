﻿using System;
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
        [Required]
        public string ImagePath { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
        public bool IsDeleted { get; set; }
    }
}
