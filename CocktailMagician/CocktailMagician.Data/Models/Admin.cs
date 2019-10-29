using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Models
{
    public class Admin
    {
        public Admin()
        {

        }
        public Admin(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =(5))]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = (5))]
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
