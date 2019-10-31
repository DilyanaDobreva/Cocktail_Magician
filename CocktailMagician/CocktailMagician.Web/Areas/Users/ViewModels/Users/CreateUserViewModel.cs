using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class CreateUserViewModel
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
        public List<SelectListItem> Role { get; set; }
        public int RoleId { get; set; }
    }
}
