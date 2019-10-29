using System;
using System.Reflection.Metadata.Ecma335;

namespace CocktailMagician.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
