using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DTOs
{
    public class BarToEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public AddressDTO Address { get; set; }
    }
}
