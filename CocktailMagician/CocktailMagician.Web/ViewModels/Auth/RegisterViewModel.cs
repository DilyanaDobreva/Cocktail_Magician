using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please specify a username")]
        [MinLength(5, ErrorMessage = "Please provide at least 5 characters")]
        [Display(Name = "Username")]
        public string RegisterUsername { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [Required]
        [Compare(nameof(RegisterPassword))]
        [DataType(DataType.Password)]
        public string ComparePassword { get; set; }
    }
}
