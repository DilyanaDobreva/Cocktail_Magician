using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class User
    {
        public User()
        {

        }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = (5))]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = (5))]
        public string Password { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public bool IsDeleted { get; set; }
    }
}
